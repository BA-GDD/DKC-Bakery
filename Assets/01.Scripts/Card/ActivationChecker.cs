using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDefine;

public class ActivationChecker : MonoBehaviour
{
    [SerializeField] private RectTransform _waitZone;

    private Vector2 _mousePos;

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
            if(CardReader.OnPointerCard.CardInfo.CardType == CardType.SKILL)
            {
                CardReader.SkillCardManagement.SetSkillCardInCardZone(CardReader.OnPointerCard);
            }
            else
            {
                CardReader.SpellCardManagement.UseAbility(CardReader.OnPointerCard);
            }
        }
        else
        {
            CardReader.OnPointerCard.SetUpCard(CardReader.GetHandPos(CardReader.OnPointerCard), true);
        }
    }

    private bool IsPointerOnCard()
    {
        return CardReader.OnPointerCard != null;
    }
}
