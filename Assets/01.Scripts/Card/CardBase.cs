using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CardDefine;
using System;

public class CardBase : MonoBehaviour
{
    [SerializeField] private float _toMovePosInSec;

    public CardInfo CardInfo => _myCardInfo;
    [SerializeField] private CardInfo _myCardInfo;

    protected bool _canUseThisCard;
    public bool CanUseThisCard => _canUseThisCard;

    public CombineLevel CombineLevel;

    [SerializeField] private Transform visualTrm;
    public Transform VisualTrm
    {
        get
        {
            return visualTrm;
        }
        set
        {
            visualTrm = value;
        }
    }

    public void SetUpCard(float moveToXPos)
    {
        _canUseThisCard = false;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveX(moveToXPos, _toMovePosInSec).SetEase(Ease.OutBack));
        seq.AppendCallback(() =>
        {
            if (CardReader.CountOfCardInHand() - 2 != -1)
            {
                CardBase frontOfThisCard = CardReader.GetCardinfoInHand(CardReader.CountOfCardInHand() - 2);
                Debug.Log(frontOfThisCard);

                if (frontOfThisCard.CardInfo.CardName == _myCardInfo.CardName &&
                    frontOfThisCard.CombineLevel == CombineLevel &&
                    frontOfThisCard.CombineLevel != CombineLevel.III)
                {
                    CardReader.CombineMaster.CombineCard(this, frontOfThisCard);
                }
                else
                {
                    Debug.Log("notCombine");
                    CardReader.CardDrawer.CanDraw = true;
                }
            }
            else
            {
                CardReader.CardDrawer.CanDraw = true;
            }
        });
    }
}
