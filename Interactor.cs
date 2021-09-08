using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public static Interactor left, right;
    public Chirality hand;

    Interactable target;
    public Interactable Target => target;

    Interactable last;

    InteractionsManager iManager;

    public GameObject model;
    public Transform climbingAnchor;
    public LayerMask climbLayer;
    private float climbRadius = 0.1f;
    private readonly Collider[] _climbColliders = new Collider[100];
    private ConfigurableJoint Joint;

    public enum State
    {
        Empty,
        Summoning,
        Hovering,
        Holding,
        Climbing
    }

    public State state;

    public float summonTime = 1.0f;
    float summonProgress;

    public Vector3 Velocity => (transform.position - lastPos);
    Vector3 lastPos;

    public UnityEvent buttonOne, buttonTwo;

    public Vector2 thumbstick;

    List<InteractorModule> modules = new List<InteractorModule>();

    public Image stateDebug;

    private void Awake()
    {
        if (hand == Chirality.Left)
        {
            left = this;
        } else if (hand == Chirality.Right)
        {
            right = this;
        }

        if (climbingAnchor == null)
        {
            climbingAnchor = this.transform;
        }
    }

    private void Start()
    {
        lastPos = transform.position;
        state = State.Empty;
        iManager = InteractionsManager.Instance;
        model.SetActive(true);

        modules = new List<InteractorModule>(gameObject.GetComponents<InteractorModule>());
        foreach (var mod in modules)
        {
            mod.Setup(this);
        }
    }

    private void Update()
    { 
        if (state == State.Empty)
        {
            if ((bool)iManager.GetInput(InputAction.GetDown, InputBinding.Grip, hand))
            {
                Summon();
                return;
            }

            var i = iManager.Target(this);
            if (i != null)
            {
                target = i;
                i.AddHover(this);
                state = State.Hovering;
            }
            else
            {
                if ((bool)iManager.GetInput(InputAction.GetDown, InputBinding.Grip, hand))
                {
                    ClimbGrab();
                }
            }
        }
        else if (state == State.Hovering)
        {
            if ((bool)iManager.GetInput(InputAction.GetDown, InputBinding.Grip, hand))
            {
                Grab(target);
                return;
            }

            var i = iManager.Target(this);
            if (i != null)
            {
                if (i != target)
                {
                    target.RemoveHover(this);
                    target = i;
                    i.AddHover(this);
                }
            }
            else
            {
                target.RemoveHover(this);
                target = null;
                state = State.Empty;
            }
        }
        else if (state == State.Holding)
        {
            if ((bool)iManager.GetInput(InputAction.GetUp, InputBinding.Grip, hand))
            {
                Release();
            } 
        }
        else if (state == State.Summoning)
        {
            if ((bool)iManager.GetInput(InputAction.GetDown, InputBinding.Grip, hand))
            {
                Release();
            }
        }
        else if (state == State.Climbing)
        {
            if ((bool)iManager.GetInput(InputAction.GetDown, InputBinding.Grip, hand))
            {
                if (Joint)
                {
                    Destroy(Joint);
                }
                state = State.Empty;
            }
        }

        UpdateInput(); 

        foreach (var mod in modules)
        {
            mod.OnUpdateEx();
        }

        if (stateDebug != null)
        {
            switch (state)
            {
                case State.Empty:
                    stateDebug.color = Color.grey;
                    break;
                case State.Hovering:
                    stateDebug = Color.cyan;
                    break;
                case State.Holding:
                    stateDebug = Color.green;
                    break;
                case State.Summoning:
                    stateDebug = Color.yellow;
                    break;
                case State.Climbing:
                    stateDebug = Color.red;
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        lastPos = transform.position;
    }

    void UpdateInput ()
    {
        //thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, hand);
        thumbstick = (Vector2)iManager.GetInput(InputAction.Get, InputBinding.Thumbstick, hand);
        
        if ((bool)iManager.GetInput(InputAction.GetDown, InputBinding.Trigger, hand))
        {
            if (state == State.Holding)
            {
                target.Interact();
            }
        }

        if ((bool)iManager.GetInput(InputAction.GetUp, InputBinding.Trigger, hand))        
        {
        }

        if ((bool)iManager.GetInput(InputAction.GetDown, InputBinding.Button01, hand))
        {
            buttonOne?.Invoke();
        }

        if ((bool)iManager.GetInput(InputAction.GetDown, InputBinding.Button02, hand))
        {
            buttonTwo?.Invoke();
        }

        foreach (var mod in modules)
        {
            mod.OnInputUpdateEx();
        }
    }

    public void Grab (Interactable _target)
    {
        if (target != null)
        {
            target.RemoveHover(this);
        }
        target = _target;
        _target.Grab(this);
        state = State.Holding;
        model.SetActive(false);

        foreach (var mod in modules)
        {
            mod.OnGrabEx();
        }
    }

    public void ClimbGrab ()
    {
        var hits = Physics.OverlapSphereNonAlloc(climbingAnchor.position, climbRadius, _climbColliders, climbLayer, QueryTriggerInteraction.Ignore);
        if (hits > 0)
        {
            Joint = gameObject.AddComponent<ConfigurableJoint>();
            Joint.xMotion = Joint.yMotion = Joint.zMotion = ConfigurableJointMotion.Locked;
            Joint.angularXMotion = Joint.angularYMotion = Joint.angularZMotion = ConfigurableJointMotion.Locked;
            Joint.anchor = transform.InverseTransformPoint(climbingAnchor.position);
            Joint.autoConfigureConnectedAnchor = false;
            var hit = _climbColliders[0];

            if (!hit.attachedRigidbody)
            {
                for (var index = 0; index < hits; index++)
                {
                    var col = _climbColliders[index];
                    if (!col)
                        break;
                    if (col.attachedRigidbody)
                    {
                        hit = col;
                        break;
                    }
                }
            }

            if (hit.attachedRigidbody)
            {
                Joint.connectedBody = hit.attachedRigidbody;
                Joint.connectedAnchor = hit.attachedRigidbody.transform.InverseTransformPoint(climbingAnchor.position);
            }
            else
            {
                Joint.connectedAnchor = climbingAnchor.position;
            }

            state = State.Climbing;
        }
    }

    public void Summon ()
    {
        if (last == null)
        {
            return;
        }

        if (summonRoutine != null)
        {
            StopCoroutine(summonRoutine);
        }

        summonRoutine = StartCoroutine(SummonRoutine());
    }

    Coroutine summonRoutine;
    IEnumerator SummonRoutine()
    {
        state = State.Summoning;
        bool cutoff = false;
        for (float f = 0; f < 1; f += Time.deltaTime / summonTime)
        {
            summonProgress = f;
            if ((bool)iManager.GetInput(InputAction.GetUp, InputBinding.Grip, hand))
            {
                cutoff = true;
                break;
            }
            yield return null;
        }

        if (!cutoff)
        {
            Grab(last);
        }

        yield break;
    }

    public void Release ()
    {
        foreach (var mod in modules)
        {
            mod.OnReleaseEx();
        }

        if (state == State.Holding && target != null)
        {
            target.Release(this);
        }

        state = State.Empty;
        last = target;
        target = null;

        if (summonRoutine != null)
        {
            StopCoroutine(summonRoutine);
        }
        model.SetActive(true);
    }
}
