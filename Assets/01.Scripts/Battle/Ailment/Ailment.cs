using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ailment
{
    protected AilmentStat _stat;
    protected Health _health;
    protected AilmentEnum _ailment;

    public Ailment(AilmentStat stat, Health health, AilmentEnum ailment)
    {
        _stat = stat;
        _health = health;
        _ailment = ailment;
    }
    public bool stacking;
    public int stack;

    public int duration;
    public abstract void UseAilment();

    public virtual void ActiveAilment(int duration)
    {
        this.duration = duration;
    }
    public virtual void Update()
    {
        duration--;
        if(duration <= 0)
        {
            _stat.CuredAilment(_ailment);
        }
    }
}