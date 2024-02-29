using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct SpikePatternInfo { public List<FlontrolSpike> spikes; }
public class Flontrol : Enemy
{
    private readonly int _playerXHash = Animator.StringToHash("playerX");

    public List<SpikePatternInfo> spikePatten;
    public bool endAnimationTrigger;
    public Action animationEvent;
    public FlontrolClapWave clapWave;

    public List<FlontrolSpike> mapSpikes;
    public List<DamageCaster> leftArmDamageCast;
    public List<DamageCaster> rightArmDamageCast;
    public List<DamageCaster> seqAttackDamageCaster;
    public List<PlatformEffector2D> platforms;
    public Transform leftHandTrm;
    public Transform rightHandTrm;

    public Transform flowerShotTransfom;
    public GameObject flower;
    private List<SpriteRenderer> _renders;

    public float animationSpeed;

    [SerializeField] private List<HealthShareInfo> _shareInfos;

    protected override void Awake()
    {
        base.Awake();
        foreach(var s in mapSpikes)
        {
            s.Bind(this);
        }

        foreach(var d in leftArmDamageCast)
        {
            d.SetOwner(this);
        }
        foreach (var d in rightArmDamageCast)
        {
            d.SetOwner(this);
        }
        foreach (var d in seqAttackDamageCaster)
        {
            d.SetOwner(this);
        }

        foreach (var s in _shareInfos)
        {
            s.health.Init(this, HealthCompo, s.healthAmount);
        }

        _renders = GetComponentsInChildren<SpriteRenderer>().ToList();
    }
    protected override void Update()
    {
        base.Update();
        AnimatorCompo.SetFloat(_playerXHash, GameManager.Instance.PlayerTrm.position.x);
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
        foreach (var s in _shareInfos)
        {
            s.health.ApplyHealth(HealthCompo.maxHealth , true);
        }
        Collider.enabled = true;
        HealthCompo.ApplyHeal(HealthCompo.maxHealth);
        CharStat.damage.AddModifier(CharStat.damage.GetValue());
        AnimatorCompo.speed = 2;
        phase++;
    }

    public RaycastHit2D IsGroundDetectedByPlayer(Vector2 playerPos) => Physics2D.Raycast(playerPos, Vector2.down, 100f, _whatIsObstacle);

}
