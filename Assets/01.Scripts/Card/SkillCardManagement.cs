using DG.Tweening;
using Google.GData.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillCardManagement : CardManagement
{
    private ExpansionList<CardBase> InCardZoneCatalogue = new ExpansionList<CardBase>();

    [Header("대기존 셋팅값")]
    [SerializeField] private Transform _cardWaitZone;
    [SerializeField] private Vector2 _normalZonePos;
    [SerializeField] private float _waitTurmValue = 85f;
    [SerializeField] private Transform _cardInfoTrm;
    private CardInfoPanel _cardInfoPanel;

    [Header("발동존 셋팅값")]
    [SerializeField] private Transform _activationCardZone;
    [SerializeField] private Vector2 _lastCardPos;
    [SerializeField] private float _activationTurmValue = 155f;

    [Header("이벤트")]
    private bool _isInChaining;
    [SerializeField] private UnityEvent _beforeChainingEvent;
    [SerializeField] private UnityEvent _afterChanningEvent;
    [SerializeField] private UnityEvent<bool> _acceptBtnSwitchEvent;

    private void Start()
    {
        InCardZoneCatalogue.ListChanged += HandleCheckAcceptBtn;
    }
    private void HandleCheckAcceptBtn(object sender, EventArgs e)
    {
        _acceptBtnSwitchEvent?.Invoke(InCardZoneCatalogue.Count != 0);
    }
    public void SetupCardsInActivationZone()
    {
        _acceptBtnSwitchEvent?.Invoke(false);
        int maxCount = InCardZoneCatalogue.Count;

        for (int i = 0; i < maxCount; i++)
        {
            float x = _lastCardPos.x - (_activationTurmValue * i);
            Vector2 targetPos = new Vector2(x, _lastCardPos.y);
            Transform selectTrm = InCardZoneCatalogue[i].transform;

            selectTrm.SetParent(_activationCardZone);

            Sequence seq = DOTween.Sequence();
            seq.Append(selectTrm.DOLocalRotate(new Vector3(0, 0, 10), 0.1f));
            seq.Append(selectTrm.DOLocalMove(targetPos, 0.5f).SetEase(Ease.InOutBack));
            seq.Join(selectTrm.DOLocalRotate(Vector3.zero, 0.5f));

            if(i == maxCount - 1)
            {
                seq.InsertCallback(1, () => ChainingSkill());
            }
        }
    }
    public void ChainingSkill()
    {
        if(!_isInChaining && InCardZoneCatalogue.Count != 0)
        {
            _beforeChainingEvent?.Invoke();
            _isInChaining = true;
        }
        else if(_isInChaining && InCardZoneCatalogue.Count == 0)
        {
            _afterChanningEvent?.Invoke();
            _isInChaining = false;

            TurnCounter.ChangeTurn();
            return;
        }

        CardBase selectCard = InCardZoneCatalogue[InCardZoneCatalogue.Count - 1];
        InCardZoneCatalogue.Remove(selectCard);

        selectCard.ActiveInfo();
        UseAbility(selectCard);
    }
    public override void UseAbility(CardBase selectCard)
    {
        selectCard.Abillity();
    }
    public void SetSkillCardInCardZone(CardBase selectCard)
    {
        selectCard.CanUseThisCard = false;

        selectCard.transform.SetParent(_cardWaitZone);
        CardReader.RemoveCardInHand(CardReader.OnPointerCard);
        InCardZoneCatalogue.Add(selectCard);

        selectCard.transform.DOScale(0.7f, 0.3f);
        GenerateCardPosition(selectCard);
        CardReader.CombineMaster.CombineGenerate();
    }
    private void GenerateCardPosition(CardBase selectCard)
    {
        int maxIdx = InCardZoneCatalogue.Count - 1;

        if (maxIdx != 0)
        {
            selectCard.transform.
            DOLocalMove(new Vector2(InCardZoneCatalogue[maxIdx - 1].transform.localPosition.x 
                                    + 85, 0), 0.3f);
        }
        else
        {
            selectCard.transform.DOLocalMove(Vector3.zero, 0.3f);
        }

        for(int i = 0; i < maxIdx; i++)
        {
            Transform selectTrm = InCardZoneCatalogue[i].transform;
            selectTrm.DOLocalMove(new Vector2(selectTrm.localPosition.x - 85f, 0), 0.3f);
        }
    }
    public void SetCardInfo(CardInfo info, bool isSet)
    {
        if(isSet)
        {
            _cardInfoPanel = PoolManager.Instance.Pop(PoolingType.CardInfoPanel) as CardInfoPanel;
            _cardInfoPanel.SetInfo(info, _cardInfoTrm);
        }
        else
        {
            _cardInfoPanel.UnSetInfo();
        }

    }
}
