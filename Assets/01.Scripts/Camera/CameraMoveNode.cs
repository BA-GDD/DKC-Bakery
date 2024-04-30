using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraMoveNode : ScriptableObject
{
    public abstract Vector3 ReTurnPoint();
    public abstract IEnumerator GetYield();
    public abstract void Init(CameraMover mover);
    public float moveNextNode;
    public float orthoGraphicSize = 5;
}