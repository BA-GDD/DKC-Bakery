using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct SpikePatternInfo { public List<FlontrolSpike> spikes; }
public class Flontrol : Enemy
{
    public List<SpikePatternInfo> spikePatten;
    public bool endAnimationTrigger;
    public Action animationEvent;
    public FlontrolClapWave clapWave;

    public List<DamageCaster> leftArmDamageCast;
    public List<DamageCaster> rightArmDamageCast;
    public List<DamageCaster> berserkAttackDamageCaster;

    public Transform flowerShotTransfom;
    public GameObject flower;
    private List<SpriteRenderer> _renders;

    public float animationSpeed;

    protected override void Awake()
    {
        base.Awake();
        foreach(var d in leftArmDamageCast)
        {
            d.SetOwner(this);
        }
        foreach (var d in rightArmDamageCast)
        {
            d.SetOwner(this);
        }
        foreach (var d in berserkAttackDamageCaster)
        {
            d.SetOwner(this);
        }

        _renders = GetComponentsInChildren<SpriteRenderer>().ToList();
    }

    public override void AnimationFinishTrigger()
    {
        endAnimationTrigger = true;
    }

    public override void SlowEntityBy(float percent)
    {
    }

    protected override void HandleDie(Vector2 direction)
    {
    }

    public void Berserk()
    {
        foreach(var sp in _renders)
        {
            sp.color = Color.red;
        }
        HealthCompo.ApplyHeal(HealthCompo.maxHealth);
        AnimatorCompo.speed = 2;
        phase++;
    }
}
