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
        target = GameManager.Instance.Player;
    }

    public override void MoveToTargetForward()
    {
        CameraController.Instance.SetFollowCam(camTrack.targetTrm, transform);
        camTrack.StartMove();
        lastMovePos = transform.position;
        camTrack.transform.SetParent(null);


        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(target.forwardTrm.position, moveDuration));
        seq.Join(DOVirtual.DelayedCall(0.25f, () => CameraController.Instance.SetFollowCam(camTrack.targetTrm, target.transform)));
        seq.OnComplete(() =>
        {
            AnimatorCompo.SetTrigger(attackTriggerAnimationHash);
        });
    }

    public override void MoveToLastPos()
    {
        base.MoveToLastPos();
        transform.DOMove(lastMovePos, moveDuration).OnComplete(() => _turnEnd = true);
    }

    public override void Attack()
    {
        AnimatorCompo.SetBool(attackAnimationHash, true);
        MoveToTargetForward();
        OnAnimationEnd += () =>
        {
            MoveToLastPos();
            AnimatorCompo.SetBool(attackAnimationHash, false);
            CameraController.Instance.SetDefaultCam();
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
        _turnEnd = false;
    }
    public override void TurnStart()
    {
    }

    public override void Spawn(Vector3 spawnPos)
    {
        AnimatorCompo.SetBool(spawnAnimationHash, true);

        transform.position = spawnPos + new Vector3(-4f,6f);
        transform.DOMoveX(spawnPos.x,1f);
        transform.DOMoveY(spawnPos.y, 1f).SetEase(Ease.InCubic).OnComplete(() => AnimatorCompo.SetBool(spawnAnimationHash,false));
    }
}
