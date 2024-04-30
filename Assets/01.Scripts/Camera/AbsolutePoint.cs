using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsolutePoint : CameraMoveNode
{
    public Vector3 pos;

    public override IEnumerator GetYield()
    {
        return null;
    }

    public override void Init(CameraMover mover)
    {
    }

    public override Vector3 ReTurnPoint()
    {
        return pos;
    }
}
