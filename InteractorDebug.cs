using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorDebug : InteractorModule
{
    public override void OnInputUpdateEx()
    {
        base.OnInputUpdateEx();

        Log.Add(1, $"Thumb: {target.thumbstick}, x100: {target.thumbstick * 100}");
    }
}
