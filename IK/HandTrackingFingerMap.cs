using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackingFingerMap : MonoBehaviour
{
    public HTFinger Hand_Start;
    public HTFinger Hand_WristRoot;
    public HTFinger Hand_ForearmStub;
    public HTFinger Hand_Thumb0;
    public HTFinger Hand_Thumb1;
    public HTFinger Hand_Thumb2;
    public HTFinger Hand_Thumb3;
    public HTFinger Hand_Index1;
    public HTFinger Hand_Index2;
    public HTFinger Hand_Index3;
    public HTFinger Hand_Middle1;
    public HTFinger Hand_Middle2;
    public HTFinger Hand_Middle3;
    public HTFinger Hand_Ring1;
    public HTFinger Hand_Ring2;
    public HTFinger Hand_Ring3;
    public HTFinger Hand_Pinky0;
    public HTFinger Hand_Pinky1;
    public HTFinger Hand_Pinky2;
    public HTFinger Hand_Pinky3;
    public HTFinger Hand_MaxSkinnable;
    public HTFinger Hand_ThumbTip;
    public HTFinger Hand_IndexTip;
    public HTFinger Hand_MiddleTip;
    public HTFinger Hand_RingTip;
    public HTFinger Hand_PinkyTip;
    public HTFinger Hand_End;

    [ContextMenu("Zero All")]
    public void ZeroAll ()
    {
        Hand_Start.offset = Vector3.zero;
        Hand_WristRoot.offset = Vector3.zero;
        Hand_ForearmStub.offset = Vector3.zero;
        Hand_Thumb0.offset = Vector3.zero;
        Hand_Thumb1.offset = Vector3.zero;
        Hand_Thumb2.offset = Vector3.zero;
        Hand_Thumb3.offset = Vector3.zero;
        Hand_Index1.offset = Vector3.zero;
        Hand_Index2.offset = Vector3.zero;
        Hand_Index3.offset = Vector3.zero;
        Hand_Middle1.offset = Vector3.zero;
        Hand_Middle2.offset = Vector3.zero;
        Hand_Middle3.offset = Vector3.zero;
        Hand_Ring1.offset = Vector3.zero;
        Hand_Ring2.offset = Vector3.zero;
        Hand_Ring3.offset = Vector3.zero;
        Hand_Pinky0.offset = Vector3.zero;
        Hand_Pinky1.offset = Vector3.zero;
        Hand_Pinky2.offset = Vector3.zero;
        Hand_Pinky3.offset = Vector3.zero;
        Hand_MaxSkinnable.offset = Vector3.zero;
        Hand_ThumbTip.offset = Vector3.zero;
        Hand_IndexTip.offset = Vector3.zero;
        Hand_MiddleTip.offset = Vector3.zero;
        Hand_RingTip.offset = Vector3.zero;
        Hand_PinkyTip.offset = Vector3.zero;
        Hand_End.offset = Vector3.zero;
    }
}
