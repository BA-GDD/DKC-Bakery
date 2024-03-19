using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public PlayerStat PlayerStat { get; private set; }
    public SkillManager Skill { get; private set; }
    private PlayerHPUI _hpUI;

    protected override void Awake()
    {
        base.Awake();

        PlayerStat = CharStat as PlayerStat;

        TurnCounter.PlayerTurnStartEvent += TurnStart;
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

    public void TurnStart()
    {
        //CameraMoveTrack track = PlayerCameraMoveDic.Instance[CardDefine.PlayerSkill.Heal];
        //CameraController.Instance.SetFollowCam(track.targetTrm, transform);
        //track.StartMove();
    }
    public void TurnEnd()
    {
    }
}
