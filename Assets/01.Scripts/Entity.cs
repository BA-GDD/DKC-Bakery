using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : PoolableMono
{
    #region components
    public Animator AnimatorCompo { get; private set; }
    public Health HealthCompo { get; private set; }
    public SpriteRenderer SpriteRendererCompo { get; private set; }
    public BattleController BattleController { get; set; }
    [field: SerializeField] public CharacterStat CharStat { get; private set; }
    #endregion

    protected int _hitAnimationHash = Animator.StringToHash("hit");
    protected int _deathAnimationHash = Animator.StringToHash("death");

    public UnityEvent<float> OnHealthBarChanged;
    public Action OnAnimationCall;
    public Action OnAnimationEnd;

    //예네 지울지 고민 좀만 해보자
    public Action<int> OnStartAttack;
    public Action OnEndAttack;

    public Transform forwardTrm;

    public Entity target;

    [SerializeField]protected Vector3 lastMovePos;
    [SerializeField]protected float moveDuration = 0.1f;


    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        HealthCompo = GetComponent<Health>();
        SpriteRendererCompo = visualTrm.GetComponent<SpriteRenderer>();
        HealthCompo.SetOwner(this);

        HealthCompo.OnHitEvent.AddListener(HandleHit);
        HealthCompo.OnDeathEvent.AddListener(HandleDie);
        HealthCompo.OnDeathEvent.AddListener(HandleCutInOnFieldMonsterList);
        HealthCompo.OnAilmentChanged.AddListener(HandleAilmentChanged);
        OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth()); //�ִ�ġ�� UI����.
        CharStat = Instantiate(CharStat); //������ ����
        CharStat.SetOwner(this);
    }

    private void HandleCutInOnFieldMonsterList()
    {
        HealthCompo.OnDeathEvent.RemoveListener(HandleCutInOnFieldMonsterList);
    }

    private void OnDestroy()
    {
        HealthCompo.OnAilmentChanged.RemoveListener(HandleAilmentChanged);
    }

    //���ῡ ���� ó��.
    private void HandleAilmentChanged(AilmentEnum ailment)
    {
        if ((ailment & AilmentEnum.Chilled) > 0) //������¸� ���ǵ� ������
        {
            //���� ���׿� ���� ����
            float resistance = (100 - CharStat.magicResistance.GetValue()) * 0.01f;
            SlowEntityBy(0.5f * resistance);
        }
        else
        {
            ReturnDefaultSpeed();
        }
    }

    protected virtual void HandleHit()
    {
        //UI����
        float currentHealth = HealthCompo.GetNormalizedHealth();
        if (currentHealth > 0)
        {
            AnimatorCompo.SetTrigger(_hitAnimationHash);
        }
        OnHealthBarChanged?.Invoke(currentHealth);
    }


    protected virtual void HandleDie()
    {
        EnemyStat es = CharStat as EnemyStat;
        Inventory.Instance.GetIngredientInThisBattle.Add(es.DropItem);
        AnimatorCompo.SetTrigger(_deathAnimationHash);
    }


    public abstract void SlowEntityBy(float percent); //���ο�� �ڽĵ��� ����.

    protected virtual void ReturnDefaultSpeed()
    {
        AnimatorCompo.speed = 1; //���� ���ǵ�� �ǵ�����.
    }

    public virtual void FreezeTime(bool isFreeze, bool isFrozenWithoutTimer = false)
    {
        if (isFreeze)
        {
            Debug.Log("Freezed");
            AnimatorCompo.speed = 0; //�ִϸ��̼� ����. �̵� ����.
        }
        else
        {
            Debug.Log("UnFreezed");
            AnimatorCompo.speed = 1;
        }
    }

    public abstract void MoveToTargetForward();
    public virtual void MoveToLastPos()
    {
        transform.DOMove(lastMovePos, moveDuration);
    }



    public override void Init()
    {

    }
}
