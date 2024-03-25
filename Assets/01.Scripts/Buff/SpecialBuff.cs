using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SpecialBuff : ScriptableObject
{
    protected Entity entity;
    private bool isComplete = false;
    public void SetOwner(Entity entity)
    {
        this.entity = entity;
    }
    public abstract void Active();
    public virtual void SetIsComplete(bool value)
    {
        isComplete = value;
        if(isComplete == true)
        {
            entity.BuffStatCompo.CompleteBuff(this);
        }
    }
}
