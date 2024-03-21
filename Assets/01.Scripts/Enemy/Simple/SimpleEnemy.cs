using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        OnAnimationCall += () => target.HealthCompo.ApplyDamage(CharStat.GetDamage(), this);
    }

    private void Start()
    {
        target = BattleController?.Player;
    }


    public override void Attack()
    {
        AnimatorCompo.SetBool(attackAnimationHash, true);
        MoveToTargetForward();
        OnAnimationEnd += () =>
        {
            MoveToOriginPos();
            AnimatorCompo.SetBool(attackAnimationHash, false);
            //CameraController.Instance.SetDefaultCam();
            OnAnimationEnd = null;

        };
    }

    public override void SlowEntityBy(float percent)
    {
    }

    public override void TurnAction()
    {
        Attack();
    }

    public override void TurnEnd()
    {
        camTrack.transform.SetParent(transform);
    }
    public override void TurnStart()
    {
        turnStatus = TurnStatus.Ready;
    }

    public override void Spawn(Vector3 spawnPos)
    {
        AnimatorCompo.SetBool(spawnAnimationHash, true);

        transform.position = spawnPos + new Vector3(-4f, 6f);
        transform.DOMoveX(spawnPos.x, 1f);
        transform.DOMoveY(spawnPos.y, 1f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            AnimatorCompo.SetBool(spawnAnimationHash, false);
            turnStatus = TurnStatus.Ready;
        });
    }

    protected override void HandleMoveToTarget()
    {
        MoveToOriginPos();
        target.HealthCompo.ApplyDamage(CharStat.GetDamage(), this);
        AnimatorCompo.SetTrigger(attackTriggerAnimationHash);
    }

    protected override void HandleMoveToOriginPos()
    {
        turnStatus = TurnStatus.End;
    }
}
