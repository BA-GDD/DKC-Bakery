using DG.Tweening;
using Particle;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Sequence = DG.Tweening.Sequence;

[Serializable]
public struct EnemyAttack
{
    public ParticleInfo attack;
}

public abstract class Enemy : Entity
{
    [SerializeField] protected EnemyAttack attackParticle;

    protected int attackAnimationHash = Animator.StringToHash("attack");
    protected int attackTriggerAnimationHash = Animator.StringToHash("attackTrigger");
    protected int spawnAnimationHash = Animator.StringToHash("spawn");

    protected EnemyVFXPlayer VFXPlayer { get; private set; }

    protected Collider2D Collider;

    public TurnStatus turnStatus;

    protected override void Awake()
    {
        base.Awake();
        VFXPlayer = GetComponent<EnemyVFXPlayer>();
        Collider = GetComponent<Collider2D>();

        if(attackParticle.attack != null)
            SetParticleInfo();

    }
    private void SetParticleInfo()
    {
        attackParticle.attack.owner = this;
        attackParticle.attack.damages = SetDamage((EnemyStat)CharStat);

    }
    private int[] SetDamage(EnemyStat stat)
    {
        List<int> list = new();
        for (int i = 0; i < stat.attackCnt; i++)
        {
            list.Add(stat.damage.GetValue() / stat.attackCnt);
        }
        return list.ToArray();
    }
    protected virtual void HandleAttackStart()
    {
        AnimatorCompo.SetBool(attackAnimationHash, true);
    }
    protected virtual void HandleAttackEnd()
    {
        CameraController.Instance.SetDefaultCam();
        AnimatorCompo.SetBool(attackAnimationHash, false);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnAttackStart += HandleAttackStart;
        OnAttackEnd += HandleAttackEnd;
        target = BattleController?.Player;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        OnAttackStart -= HandleAttackStart;
        OnAttackEnd -= HandleAttackEnd;
    }
    public abstract void Attack();

    public virtual void TurnStart()
    {
        Collider.enabled = false;
    }
    public abstract void TurnAction();
    public virtual void TurnEnd()
    {
        Collider.enabled = true;
    }

    public virtual void Spawn(Vector3 spawnPos)
    {
        SpriteRendererCompo.material.SetFloat("_dissolve_amount", 0);

        AnimatorCompo.SetBool(spawnAnimationHash, true);

        transform.position = spawnPos + new Vector3(-4f, 6f);
        transform.DOMoveX(spawnPos.x, 1f);
        transform.DOMoveY(spawnPos.y, 1f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            AnimatorCompo.SetBool(spawnAnimationHash, false);
            turnStatus = TurnStatus.Ready;
        });
    }
    public void MoveFormation(Vector3 pos)
    {
        transform.DOMove(pos, 1f);
    }

    [ContextMenu("TurnStart")]
    private void TestTurnStart() => TurnStart();
    [ContextMenu("TurnAction")]
    private void TestTurnAction() => TurnAction();
    [ContextMenu("TurnEnd")]
    private void TestTurnEnd() => TurnEnd();

    public void SelectedOnAttack(CardBase selectCard)
    {
        BattleController.SelectPlayerTarget(selectCard, this);
    }
}