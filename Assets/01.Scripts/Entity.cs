using DG.Tweening;
using System;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
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
    public Action<int> OnStartAttack;
    public Action OnEndAttack;
    public UnityEvent OnDieEvent;

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
        OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth()); //최대치로 UI변경.
        CharStat = Instantiate(CharStat); //복제본 생성
        CharStat.SetOwner(this);
    }

    private void HandleCutInOnFieldMonsterList()
    {
        BattleController.onFieldMonsterList.Remove(this as Enemy);
        HealthCompo.OnDeathEvent.RemoveListener(HandleCutInOnFieldMonsterList);
    }

    private void OnDestroy()
    {
        HealthCompo.OnAilmentChanged.RemoveListener(HandleAilmentChanged);
    }

    //동결에 따른 처리.
    private void HandleAilmentChanged(Ailment ailment)
    {
        if ((ailment & Ailment.Chilled) > 0) //동결상태면 스피드 느리게
        {
            //마법 저항에 따라 적게
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
        //UI갱신
        float currentHealth = HealthCompo.GetNormalizedHealth();
        if (currentHealth <= 0)
        {
            OnDieEvent?.Invoke();
        }
        else
        {
            AnimatorCompo.SetTrigger(_hitAnimationHash);
        }
        OnHealthBarChanged?.Invoke(currentHealth);
    }


    protected virtual void HandleDie()
    {
        AnimatorCompo.SetTrigger(_deathAnimationHash);

    }


    public abstract void SlowEntityBy(float percent); //슬로우는 자식들이 구현.

    protected virtual void ReturnDefaultSpeed()
    {
        AnimatorCompo.speed = 1; //원래 스피드로 되돌리기.
    }

    public virtual void FreezeTime(bool isFreeze, bool isFrozenWithoutTimer = false)
    {
        if (isFreeze)
        {
            Debug.Log("Freezed");
            AnimatorCompo.speed = 0; //애니메이션 정지. 이동 정지.
        }
        else
        {
            Debug.Log("UnFreezed");
            AnimatorCompo.speed = 1;
        }
    }

    public abstract void MoveToTargetForward();
    public void MoveToLastPos()
    {
        transform.DOMove(lastMovePos, moveDuration);
    }
}
