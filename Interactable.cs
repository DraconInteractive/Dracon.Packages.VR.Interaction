using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string ID;
    
    public GameObject model;
    public GrabPoint[] grabPoints;

    [HideInInspector]
    public GrabPoint firstGrab, secondGrab;

    public int ActiveInteractors => (firstGrab == null) ? 0 : (secondGrab == null) ? 1 : 2;
    public enum GrabState
    {
        Idle,
        InLeft,
        InRight,
        InBoth
    }

    public GrabState State
    {
        get
        {
            var activeInteractors = ActiveInteractors;

            if (activeInteractors == 0)
            {
                return GrabState.Idle;
            } 
            else
            {
                bool l = false;
                bool r = false;

                if (activeInteractors == 1)
                {
                    l = firstGrab.interactor == Interactor.left;
                    r = firstGrab.interactor == Interactor.right;
                }
                else if (activeInteractors == 2)
                {
                    l = (firstGrab.interactor == Interactor.left) || (secondGrab.interactor == Interactor.left);
                    r = (firstGrab.interactor == Interactor.right) || (secondGrab.interactor == Interactor.right);
                }

                if (l && r)
                {
                    return GrabState.InBoth;
                } 
                else if (l)
                {
                    return GrabState.InLeft;
                } 
                else if (r)
                {
                    return GrabState.InRight;
                }
            }

            return GrabState.Idle;
        }
    }

    List<InteractableModule> modules = new List<InteractableModule>();

    public UnityEvent onGrab, onRelease, onInteract;

    private void Start()
    {
        var m = GetComponents<InteractableModule>();
        modules = new List<InteractableModule>(m);

        foreach (var module in modules)
        {
            module.Setup(this);
        }

        foreach (var g in grabPoints)
        {
            g.Setup(this);
        }
    }

    private void Update()
    {
        foreach (var module in modules)
        {
            module.UpdateEx();
        }
    }

    private void FixedUpdate()
    {
        foreach (var module in modules)
        {
            module.FixedUpdateEx();
        }
    }

    private void LateUpdate ()
    {
        foreach (var module in modules)
        {
            module.LateUpdateEx();
        }
    }

    private void OnValidate()
    {
        if (GUI.changed)
        {
            gameObject.name = $"Interactable ({ID})";

            if (grabPoints != null && grabPoints.Length > 0)
            {
                foreach (var g in grabPoints)
                {
                    g.name = $"Grab Point ({g.transform.GetSiblingIndex()})";
                }
            }
        }
    }

    private void OnEnable()
    {
        InteractionsManager.Interactables.Add(this);

        foreach (var g in grabPoints)
        {
            g.ResetPoint();
        }
    }

    private void OnDisable()
    {
        InteractionsManager.Interactables.Remove(this);
    }

    private void OnDestroy()
    {
        foreach (var module in modules)
        {
            module.OnDestroyEx();
        }
    }

    public void PointGrab (GrabPoint point)
    {
        onGrab?.Invoke();

        foreach (var module in modules)
        {
            module.OnGrab(point);
        }

        if (firstGrab == null) 
        {
            firstGrab = point;
        }
        else
        {
            secondGrab = point;
        }
    }

    public void PointRelease (GrabPoint point)
    {
        onRelease?.Invoke();

        if (firstGrab == point)
        {
            if (secondGrab != null)
            {
                firstGrab = secondGrab;
                secondGrab = null;
            } else
            {
                firstGrab = null;
            }
        } 
        else
        {
            secondGrab = null;
        }

        foreach (var module in modules)
        {
            module.OnRelease(point);
        }
    }

    public void PointInteract (GrabPoint point)
    {
        onInteract?.Invoke();

        foreach (var module in modules)
        {
            module.OnInteract(point);
        }
    }
}
