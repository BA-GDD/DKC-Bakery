using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGunSkill : CardBase, ISkillEffectAnim
{
    private Color minimumColor = new Color(255, 255, 255, .1f);
    private Color maxtimumColor = new Color(255, 255, 255, 1.0f);

    private float yPos;
    private Entity target;

    public override void Abillity()
    {
        CameraController.Instance.SetTransitionTime(1f);
        CameraController.Instance.GetVCam().SetCamera(Player.transform.position, 4.9f);


        IsActivingAbillity = true;

        target = Player.GetSkillTargetEnemyList[this][0];

        yPos = Player.transform.position.y;
        Player.transform.DOMoveY(target.transform.position.y, 0.1f).OnComplete(() => Player.UseAbility(this));
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;

        foreach (var e in battleController.onFieldMonsterList)
        {
            if (Player.GetSkillTargetEnemyList[this].Contains(e)) continue;
            e.SpriteRendererCompo.DOColor(minimumColor, 0.5f);
        }
    }


    public void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(this, Player.forwardTrm.position);
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo, (int)CombineLevel);
        Player.transform.DOMoveY(yPos, 0.1f).OnComplete(() =>
        {
            IsActivingAbillity = false;
        });
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;

        foreach (var e in battleController.onFieldMonsterList)
        {
            if (Player.GetSkillTargetEnemyList[this].Contains(e) || e == null) continue;
            e.SpriteRendererCompo.DOColor(maxtimumColor, 0.5f);
        }
    }
}
