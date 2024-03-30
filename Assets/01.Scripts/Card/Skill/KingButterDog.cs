using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingButterDog : Enemy
{
    protected override void Start()
    {
        base.Start();
        target = BattleController.Player;
    }
    public override void Attack()
    {
    }

    public override void SlowEntityBy(float percent)
    {
    }

    public override void TurnAction()
    {
        MoveToTargetForward();
    }

    public override void MoveToTargetForward()
    {
        lastMovePos = transform.position;


        //seq.Append(transform.DOMove(target.forwardTrm.position, moveDuration));
        Vector2 screenPos = MaestrOffice.GetWorldPosToScreenPos(transform.position);
        Vector2 pos = MaestrOffice.GetScreenPosToWorldPos(new Vector2(Screen.width + 30, screenPos.y));
        pos.y = transform.position.y;

        Vector3 jumpPos;
        jumpPos.y = target.transform.position.y;
        
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOJump(transform.position , 3, 1, 1f));
        seq.Append(transform.DOMove(pos, 4));
        seq.OnComplete(OnMoveTarget.Invoke);
    }

    public override void Spawn(Vector3 spawnPos)
    {
        SpriteRendererCompo.material.SetFloat("_dissolve_amount", 0);

        AnimatorCompo.SetBool(spawnAnimationHash, true);

        transform.position = spawnPos + new Vector3(-6f, 0);
        transform.DOMoveX(spawnPos.x, 1f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            AnimatorCompo.SetBool(spawnAnimationHash, false);
            turnStatus = TurnStatus.Ready;
        });
    }
    public override void MoveToOriginPos()
    {
        transform.DOMove(lastMovePos, 1).OnComplete(OnMoveOriginPos.Invoke);
    }


    protected override void HandleEndMoveToOriginPos()
    {
        turnStatus = TurnStatus.End;
    }

    protected override void HandleEndMoveToTarget()
    {
        Vector2 screenPos = MaestrOffice.GetWorldPosToScreenPos(transform.position);
        transform.position = MaestrOffice.GetScreenPosToWorldPos(new Vector2(-30, screenPos.y));
        MoveToOriginPos();
    }
}
