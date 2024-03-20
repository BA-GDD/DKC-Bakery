using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}
public class Player : Entity
{
    private readonly int _moveHash = Animator.StringToHash("Move");
    private readonly int _abilityHash = Animator.StringToHash("Ability");

    public PlayerStat PlayerStat { get; private set; }
    public PlayerVFXManager VFXManager { get; private set; }
    private PlayerHPUI _hpUI;

    private AnimatorOverrideController animatorOverrideController;
    private AnimationClipOverrides clipOverrides;


    protected override void Awake()
    {
        base.Awake();

        PlayerStat = CharStat as PlayerStat;
        VFXManager = FindObjectOfType<PlayerVFXManager>();
    }
    protected void Start()
    {
        //Skill = SkillManager.Instance;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //_hpUI = UIManager.Instance.CanvasTrm.GetComponentInChildren<PlayerHPUI>();
            //Debug.Log(_hpUI);
            //HealthCompo.OnDamageEvent += _hpUI.SetHpOnUI;
        }

        animatorOverrideController = new AnimatorOverrideController(AnimatorCompo.runtimeAnimatorController);
        AnimatorCompo.runtimeAnimatorController = animatorOverrideController;

        clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(clipOverrides);
    }

    protected void OnDisable()
    {
        if (_hpUI != null)
            HealthCompo.OnDamageEvent -= _hpUI.SetHpOnUI;
    }



    public void AnimationEndTrigger()
    {
    }

    protected override void HandleDie()
    {
    }

    public override void SlowEntityBy(float percent)
    {
    }

    public override void MoveToTargetForward()
    {
    }

    public void UseAbility(CardBase card,bool isMove = false)
    {
        clipOverrides["UseAbility"] = card.CardInfo.abilityAnimation;
        animatorOverrideController.ApplyOverrides(clipOverrides);

        AnimatorCompo.SetBool(_abilityHash, true);
        AnimatorCompo.SetBool(_moveHash, isMove);
    }
    public void EndAbility()
    {
        AnimatorCompo.SetBool(_abilityHash, false);
        AnimatorCompo.SetBool(_moveHash, false);
    }
}
