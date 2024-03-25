using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDefine;

public class RestSpell : CardBase, ISkillEffectAnim
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        Player.UseAbility(this);
        Player.OnAnimationCall += HandleAnimationCall;
        Player.VFXManager.OnEndEffectEvent += HandleEffectEnd;
    }

    public void HandleAnimationCall()
    {
        Player.VFXManager.PlayParticle(CardInfo);
        StartCoroutine(SpellCor());
        Player.OnAnimationCall -= HandleAnimationCall;
    }

    public void HandleEffectEnd()
    {
        Player.EndAbility();
        Player.VFXManager.EndParticle(CardInfo);
        IsActivingAbillity = false;
        Player.VFXManager.OnEndEffectEvent -= HandleEffectEnd;
    }

    private IEnumerator SpellCor()
    {
        yield return new WaitForSeconds(1f);
        // 다시 만들어야함
        List<CardBase> handCards = new List<CardBase>();
        handCards = CardReader.GetHandCards();
        Queue<CombineLevel> combineLvQueue = new Queue<CombineLevel>();
        foreach (var card in handCards)
        {
            CardReader.CardDrawer.DrawCard(1);
            CardReader.GetCardinfoInHand(handCards.Count + 1).CombineLevel = handCards[0].CombineLevel;
            CardReader.RemoveCardInHand(handCards[0]);
        }
    }
}
