using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if FOCUS_3
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.VRModuleManagement;
#endif
public enum Chirality
{
    None,
    Left,
    Right
}
public enum InputAction
{
    Get,
    GetDown,
    GetUp
}
public enum InputBinding
{
    Trigger,
    Grip,
    Button01,
    Button02,
    Thumbstick
}
public class InteractionsManager : MonoBehaviour
{
    public static InteractionsManager Instance;
    public static List<Interactable> Interactables = new List<Interactable>();

    private void Awake()
    {
        Instance = this;
    }

    public Interactable Target (Interactor hand)
    {
        float _dist = Mathf.Infinity;
        Interactable _return = null;

        foreach (var i in Interactables)
        {
            float dist = Vector3.Distance(i.transform.position, hand.transform.position);
            if (dist < _dist && dist < i.interactionRange && !i.InHand)
            {
                _dist = dist;
                _return = i;
            }
        }

        return _return;
    }

#if QUEST_2
    public OVRInput.Controller Hand (Chirality _hand)
    {
        switch (_hand)
        {
            case Chirality.None:
                return OVRInput.Controller.None;
            case Chirality.Left:
                return OVRInput.Controller.LTouch;
            case Chirality.Right:
                return OVRInput.Controller.RTouch;
        }
        return OVRInput.Controller.None;
    }

    public OVRInput.Button Binding (InputBinding _binding)
    {
        switch (_binding)
        {
            case InputBinding.Trigger:
                return OVRInput.Button.PrimaryIndexTrigger;
            case InputBinding.Grip:
                return OVRInput.Button.PrimaryHandTrigger;
            case InputBinding.Button01:
                return OVRInput.Button.One;
            case InputBinding.Button02:
                return OVRInput.Button.Two;
        }
        return OVRInput.Button.Any;
    }

    public object GetInput (InputAction _action, InputBinding _binding, Chirality _hand) 
    {
        OVRInput.Controller controller = Hand(_hand);
        OVRInput.Button binding = Binding(_binding);

        switch (_action)
        {
            case InputAction.Get:
                if (_binding == InputBinding.Thumbstick)
                {
                    return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
                }
                else
                {
                    return OVRInput.Get(binding, controller);
                }
            case InputAction.GetDown:
                if (_binding == InputBinding.Thumbstick)
                {
                    return null;
                }
                else
                {
                    return OVRInput.GetDown(binding, controller);
                }
            case InputAction.GetUp:
                if (_binding == InputBinding.Thumbstick)
                {
                    return null;
                }
                else
                {
                    return OVRInput.GetUp(binding, controller);
                }
        }
        return null;
    }
#elif FOCUS_3
    public HandRole Hand (Chirality _hand)
    {
        switch (_hand)
        {
            case Chirality.None:
                return HandRole.Invalid;
            case Chirality.Left:
                return HandRole.LeftHand;
            case Chirality.Right:
                return HandRole.RightHand;
        }
        return HandRole.Invalid;
    }

    public ControllerButton Binding (InputBinding _binding)
    {
        switch (_binding)
        {
            case InputBinding.Trigger:
                return ControllerButton.Trigger;
            case InputBinding.Grip:
                return ControllerButton.Grip;
            case InputBinding.Button01:
                return ControllerButton.AKey;
            case InputBinding.Button02:
                return ControllerButton.BKey;
        }
        return OVRInput.Button.Any;
    }

    public object GetInput (InputAction _action, InputBinding _binding, Chirality _hand) 
    {
        OVRInput.Controller controller = Hand(_hand);
        OVRInput.Button binding = Binding(_binding);

        switch (_action)
        {
            case InputAction.Get:
                if (_binding == InputBinding.Thumbstick)
                {
                    return ViveInput.GetPadAxisEx(hand);
                }
                else
                {
                    return ViveInput.GetPressEx(hand,binding);
                }
            case InputAction.GetDown:
                if (_binding == InputBinding.Thumbstick)
                {
                    return null;
                }
                else
                {
                    return ViveInput.GetPressDownEx(hand, binding);
                }
            case InputAction.GetUp:
                if (_binding == InputBinding.Thumbstick)
                {
                    return null;
                }
                else
                {
                    return ViveInput.GetPressUp(hand, binding);
                }
        }
        return null;
    }
#endif
}
