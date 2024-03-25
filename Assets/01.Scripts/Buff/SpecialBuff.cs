using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SpecialBuff : ScriptableObject
{
    protected Entity entity;
    public void SetOwner(Entity entity)
    {
        this.entity = entity;
    }
    public abstract void Active();
}
