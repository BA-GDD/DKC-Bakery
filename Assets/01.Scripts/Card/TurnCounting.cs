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
    }

    public void GameStartEvent()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => CardReader.CardDrawer.DrawCard(5));
        seq.AppendCallback(ToPlayerTurnChanging);
    }

    public void ToPlayerTurnChanging()
    {
        _selectTrm = _toPTChangingText;
        TurnChanging();
    }

    public void ToEnemyTurnChanging()
    {
        _selectTrm = _toETChangingText;
        TurnChanging();
    }

    private void TurnChanging()
    {
        _selectTrm.transform.localPosition = _startPos.localPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(_selectTrm.DOLocalMove(_normalPos.localPosition, 0.5f).SetEase(Ease.OutCubic));
        seq.AppendInterval(0.5f);
        seq.Append(_selectTrm.DOLocalMove(_endPos.localPosition, 0.5f).SetEase(Ease.InCubic));
        seq.AppendCallback(TurnCounter.ChangeTurn);
    }
}
