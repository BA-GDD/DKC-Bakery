using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System;

public enum CardProductionType
{
    Hover,
    Select
}

public class CardProductionMaster : MonoBehaviour 
{
    private List<CardProductionRecord> _recordList = new();

    private Dictionary<CardProductionType, Action<Transform, Tween>> _toPlayActionDic = new();
    private Dictionary<CardProductionType, Action<Transform>> _toQuitActionDic = new();
    [SerializeField] private float _onTweeningEasingTime;

    [Header("카드 선택")]
    [SerializeField] private float _onSelectScaleValue;
    [SerializeField] private float _shadowMovingValue;
    [SerializeField] private float _shadowAppearTime;
    private Vector2 _onSelectNormalScale;
    private Vector2 _onSelectNormalShadowPos;

    [Header("카드 아이들")]
    private List<CardBase> _onHandCardList = new List<CardBase>();
    private float _onPointerInCardValue;

    private void Start()
    {
        foreach(CardProductionType type in Enum.GetValues(typeof(CardProductionType)))
        {
            switch (type)
            {
                case CardProductionType.Hover:
                    break;
                case CardProductionType.Select:
                    _toPlayActionDic.Add(type, OnSelectCard);
                    _toQuitActionDic.Add(type, QuitSelectCard);
                    break;
            }
        }
    }

    public void PlayProduction(CardProductionType productionType, Transform cardTrm)
    {
        foreach(CardProductionRecord reco in _recordList)
        {
            if(reco.IsSameType(productionType))
            {
                reco.Kill();
                _toQuitActionDic[productionType]?.Invoke(cardTrm);
                _recordList.Remove(reco);

                break;
            }
        }

        CardProductionRecord record = new CardProductionRecord(productionType, cardTrm);
        _recordList.Add(record);

        _toPlayActionDic[productionType]?.Invoke(cardTrm, record.OnPlayingTween);
    }

    #region Hover
    
    #endregion

    #region Select
    private void OnSelectCard(Transform cardTrm, Tween tween)
    {
        cardTrm.rotation = Quaternion.identity;

        RectTransform cardTransform = cardTrm as RectTransform;
        cardTransform.SetAsLastSibling();

        Sequence cardSelectSequence = DOTween.Sequence();
        tween = cardSelectSequence;

        Transform shadowTrm = GetShadow(cardTrm);

        _onSelectNormalScale = cardTrm.localScale;
        _onSelectNormalShadowPos = shadowTrm.localPosition;

        cardSelectSequence.Append(
        cardTrm.DOScale(cardTrm.localScale * _onSelectScaleValue, _onTweeningEasingTime).
        SetEase(Ease.OutBounce));

        cardSelectSequence.Append(
        shadowTrm.DOLocalMoveY(shadowTrm.localPosition.y - _shadowMovingValue, _shadowAppearTime));
    }
    private void QuitSelectCard(Transform cardTrm)
    {
        Transform shadowTrm = GetShadow(cardTrm);

        cardTrm.localScale = _onSelectNormalScale * _onSelectScaleValue;
        shadowTrm.localPosition = new Vector3(shadowTrm.localPosition.x,
                                  _onSelectNormalShadowPos.y - _shadowMovingValue);
    }
    #endregion

    #region CardIdle
    public void OnCardIdling(CardBase cardBase)
    {
        cardBase.CardIdlingAddValue = UnityEngine.Random.Range(-4, 4);
        _onHandCardList.Add(cardBase);
    }
    public void QuitCardling(CardBase cardBase)
    {
        _onHandCardList.Remove(cardBase);
    }
    #endregion

    private void Update()
    {
        foreach(var card in _onHandCardList)
        {
            if(!card.OnPointerInCard && card.CanUseThisCard)
            {
                float sineX = Mathf.Sin(Time.time + card.CardIdlingAddValue);
                float cosineY = Mathf.Cos(Time.time + card.CardIdlingAddValue);

                card.transform.eulerAngles = new Vector3(sineX, cosineY, 0) * 20;
            }
            else if(card.OnPointerInCard && card.CanUseThisCard)
            {
                Vector3 mouse = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
                Vector3 offset = card.transform.transform.localPosition - mouse;

                float tiltX = offset.y * -1;
                float tiltY = offset.x;

                card.transform.localRotation = Quaternion.Euler(new Vector3(tiltX, tiltY, 0) * _onPointerInCardValue);
            }
        }
    }

    private Transform GetShadow(Transform cardTrm)
    {
        return cardTrm.Find("ShadowVisual");
    }
}
