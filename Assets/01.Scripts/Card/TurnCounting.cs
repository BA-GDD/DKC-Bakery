using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnCounting : MonoBehaviour
{
    [Header("ÂüÁ¶")]
    [SerializeField] private Transform _toPTChangingText;
    [SerializeField] private Transform _toETChangingText;
    private Transform _selectTrm;

    [Header("º¤ÅÍ")]
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _normalPos;
    [SerializeField] private Transform _endPos;

    private void Start()
    {
        CardReader.SetDeck(MapManager.Instanace.SelectDeck);
        Debug.Log(MapManager.Instanace.SelectDeck);
        Debug.Log(CardReader.CountOfCardInDeck());
        TurnCounter.PlayerTurnStartEvent += ToPlayerTurnChanging;
        CardReader.CardDrawer.DrawCard(5);
        ToPlayerTurnChanging(true);
    }

    public void ToPlayerTurnChanging(bool isTurnChange)
    {
        _selectTrm = _toPTChangingText;
        TurnChanging(isTurnChange);
    }

    public void ToEnemyTurnChanging(bool isTurnChange)
    {
        _selectTrm = _toETChangingText;
        TurnChanging(isTurnChange);
    }

    private void TurnChanging(bool isTurnChange)
    {
        _selectTrm.transform.localPosition = _startPos.localPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(_selectTrm.DOLocalMove(_normalPos.localPosition, 0.5f).SetEase(Ease.OutCubic));
        seq.AppendInterval(0.5f);
        seq.Append(_selectTrm.DOLocalMove(_endPos.localPosition, 0.5f).SetEase(Ease.InCubic));
        if(isTurnChange)
            seq.AppendCallback(TurnCounter.ChangeTurn);
    }
}
