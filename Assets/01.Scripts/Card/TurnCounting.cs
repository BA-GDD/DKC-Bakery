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
    [SerializeField] private Transform _turnChaingLabel;
    private Transform _selectTrm;

    [Header("º¤ÅÍ")]
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _normalPos;
    [SerializeField] private Transform _endPos;

    private void Start()
    {
        if(MapManager.Instanace.SelectStageData.stageType == StageType.Mission)
        {
            CardReader.SetDeck(MapManager.Instanace.SelectStageData.missionDeck);
        }
        else
        {
            CardReader.SetDeck(MapManager.Instanace.SelectDeck);
        }
        TurnCounter.PlayerTurnStartEvent += ToPlayerTurnChanging;
    }

    public void BattleStart()
    {
        TurnCounter.Init();

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => ToPlayerTurnChanging(false));
        seq.AppendCallback(() => CardReader.CardDrawer.DrawCard(5));
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
        seq.Join(_turnChaingLabel.DOScaleY(1, 0.4f));
        seq.AppendInterval(0.5f);
        seq.Append(_selectTrm.DOLocalMove(_endPos.localPosition, 0.5f).SetEase(Ease.InCubic));
        seq.Join(_turnChaingLabel.DOScaleY(0, 0.4f));
        if (isTurnChange)
            seq.AppendCallback(TurnCounter.ChangeTurn);
    }
}
