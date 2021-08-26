using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string ID;
    public float interactionRange;
    public GameObject model, hover, handL, handR;

    public bool InHand => interactor != null;

    List<InteractableModule> modules = new List<InteractableModule>();
    List<object> hovering = new List<object>();
    public Interactor interactor;

    public UnityEvent onGrab, onRelease, onInteract;

    private void Start()
    {
        var m = GetComponents<InteractableModule>();
        modules = new List<InteractableModule>(m);

        foreach (var module in modules)
        {
            module.Setup(this);
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
        }
    }

    private void OnEnable()
    {
        InteractionsManager.Interactables.Add(this);

        hovering.Clear();

        hover.SetActive(false);
        handL.SetActive(false);
        handR.SetActive(false);
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
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

    [ContextMenu("Setup")]
    public void Setup ()
    {
        var _model = transform.Find("Model");
        if (_model == null)
        {
            GameObject go = new GameObject("Model");
            go.transform.SetParent(this.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            model = go;
        } 
        else
        {
            model = _model.gameObject;
        }

        var _hover = transform.Find("Hover");

        if (_hover == null)
        {
            GameObject go = new GameObject("Hover");
            go.transform.SetParent(this.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            hover = go;
        }
        else
        {
            hover = _hover.gameObject;
        }

        var _oldhand = transform.Find("Hand");
        if (_oldhand != null) 
        {
            _oldhand.name = "LHand";
            handL = _oldhand.gameObject;
        }

        var _lhand = transform.Find("LHand");
        if (_lhand == null)
        {
            GameObject go = new GameObject("LHand");
            go.transform.SetParent(this.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            handL = go;
        }
        else
        {
            handL = _lhand.gameObject;
        }

        var _rhand = transform.Find("RHand");
        if (_rhand == null) 
        {
            GameObject go = new GameObject("RHand");
            go.transform.SetParent(this.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;

            handR = go;
        }
        else
        {
            handR = _rhand.gameObject;
        }
    }
#endif

    public void Grab (Interactor owner)
    {
        if (interactor != null)
        {
            interactor.Release();
        }
        interactor = owner;
        if (owner == Interactor.left)
        {
            handL.SetActive(true);
        }
        else if (owner == Interactor.right)
        {
            handR.SetActive(true);
        }
        handL.SetActive(true);
        onGrab?.Invoke();
        foreach (var module in modules)
        {
            module.OnGrab();
        }
    }

    public void Release (Interactor owner)
    {
        onRelease?.Invoke();
        foreach (var module in modules)
        {
            module.OnRelease();
        }
        handL.SetActive(false);
        handR.SetActive(false);
        interactor = null;
    }

    public void Interact ()
    {
        foreach (var module in modules)
        {
            module.OnInteract();
        }
    }

    public void AddHover (object owner)
    {
        if (!hovering.Contains(owner))
        {
            hovering.Add(owner);
        }

        hover.SetActive(hovering.Count > 0);
    }

    public void RemoveHover(object owner)
    {
        if (hovering.Contains(owner))
        {
            hovering.Remove(owner);
        }

        hover.SetActive(hovering.Count > 0);
    }
}
