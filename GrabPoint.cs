using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabPoint : MonoBehaviour
{
    public float interactionRange;
    public Vector3 posOffset;
    public Quaternion rotOffset;

    public GameObject hover, handL, handR;
    public Interactor interactor;
    public bool InHand => interactor != null;
    [HideInInspector]
    public Interactable owner;
    List<object> hovering = new List<object>();

    public UnityEvent onGrab, onRelease, onInteract;

    public void Setup (Interactable i)
    {
        owner = i;
    }

    public void ResetPoint ()
    {
        hovering.Clear();
        hover.SetActive(false);
        handL.SetActive(false);
        handR.SetActive(false);
    }

    public void Grab(Interactor _owner)
    {
        if (interactor != null)
        {
            interactor.Release();
        }
        interactor = _owner;
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

        owner.PointGrab(this);
    }

    public void Release(Interactor _owner)
    {
        onRelease?.Invoke();

        owner.PointRelease(this);

        handL.SetActive(false);
        handR.SetActive(false);
        interactor = null;
    }

    public void Interact()
    {
        onInteract?.Invoke();

        owner.PointInteract(this);
    }

    public void AddHover(object owner)
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (interactionRange != 0)
        {
            Gizmos.DrawWireSphere(transform.position, interactionRange);
        }
    }

    [ContextMenu("Set Default Offset")]
    public void SetDefaultOffset ()
    {
        posOffset = -transform.localPosition;
        rotOffset = transform.localRotation;
    }
#endif
}
