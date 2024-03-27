using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDefine;
using UnityEngine.EventSystems;
using System;

public class ActivationChecker : MonoBehaviour
{
    [SerializeField] private RectTransform _waitZone;
    private Vector2 _mousePos;

    private int _selectIDX;

    public bool IsMouseInWaitZone()
    {
        _mousePos = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
        return UIFunction.IsMouseInRectTransform(_mousePos, _waitZone);
    }

    private void Update()
    {
        if (!IsPointerOnCard()) return;

        CheckActivation();
        BindMouse();
    }

    private void BindMouse()
    {
        if (Input.GetMouseButton(0) && CardReader.OnBinding)
        {
            CardReader.OnPointerCard.transform.position =
            MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
        }
    }

    private void CheckActivation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selectIDX = CardReader.GetIdx(CardReader.OnPointerCard);
            CardReader.OnBinding = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            CardReader.OnBinding = false;
            Activation();
            CardReader.OnPointerCard = null;
        }
    }

    private void Activation()
    {
        if (IsMouseInWaitZone())
        {
            if(!CostCalculator.CanUseCost(CardReader.OnPointerCard.CardInfo.AbillityCost, CardReader.OnPointerCard.CardInfo.CardType == CardType.SKILL))
            {
                CardReader.InGameError.ErrorSituation("�ڽ�Ʈ�� �����մϴ�!");
                CardReader.OnPointerCard.SetUpCard(CardReader.GetHandPos(CardReader.OnPointerCard), true);
                return;
            }
            
            CostCalculator.UseCost(CardReader.OnPointerCard.CardInfo.AbillityCost, CardReader.OnPointerCard.CardInfo.CardType == CardType.SKILL);

            if (CardReader.OnPointerCard.CardInfo.CardType == CardType.SKILL)
            {
                CardReader.SkillCardManagement.SetSkillCardInCardZone(CardReader.OnPointerCard);
            }
            else
            {
                CardReader.SpellCardManagement.UseAbility(CardReader.OnPointerCard);
            }
        }
        else //����
        {
            if(CardReader.GetIdx(CardReader.OnPointerCard) == _selectIDX
            || CardReader.OnPointerCard == CardReader.ShufflingCard)
            {
                CardReader.OnPointerCard.SetUpCard(CardReader.GetHandPos(CardReader.OnPointerCard), true);
                return;
            }

            if(!CostCalculator.CanUseCost(1, true))
            {
                CardReader.ShuffleInHandCard(CardReader.OnPointerCard, CardReader.ShufflingCard);
                CardReader.InGameError.ErrorSituation("�ڽ�Ʈ�� �����մϴ�!");
                CardReader.OnPointerCard.SetUpCard(CardReader.GetHandPos(CardReader.OnPointerCard), true);
                return;
            }

            CostCalculator.UseCost(1, true);
            CardReader.OnPointerCard.SetUpCard(CardReader.GetHandPos(CardReader.OnPointerCard), true);
        }
    }

    private bool IsPointerOnCard()
    {
        return CardReader.OnPointerCard != null;
    }
}
