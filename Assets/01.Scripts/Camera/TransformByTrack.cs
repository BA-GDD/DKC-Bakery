using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformByPoint : CameraMoveNode
{
    public Vector3 testTemp;
    
    public Vector3 direction;
    public Transform trm;
    public override Vector3 ReTurnPoint()
    {
        if (trm == null)
            return testTemp + direction;
        return trm.position + direction;
    }

    public override void Init(CameraMover mover)
    {
        trm = mover.trm;
    }

    public override IEnumerator GetYield()
    {
        return null;
    }
}
