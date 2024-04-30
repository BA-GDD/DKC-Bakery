using ExtensionFunction;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardShameSetter : CardSetter
{
    [SerializeField] private Sprite[] _shameIconSprite;
    [SerializeField] private string[] _shameTypeName;

    [SerializeField] private Transform _cardShameTrm;
    [SerializeField] private CardShameElement _cardShameElement;
    [SerializeField] private TextMeshProUGUI _gookBapLevelText;

    public (Sprite, string, int, int, string) GetShameDataGroup(CardShameData shameData)
    {
        int shameType = (int)shameData.cardShameType;

        return (_shameIconSprite[shameType], _shameTypeName[shameType],
                shameData.currentShame, shameData.afterShame, shameData.info);
    }

    private (float, float) GetGookBapShame(List<CardShameData> dataList)
    {
        int currentShame = dataList.Find(x => (x.cardShameType == CardShameType.Damage)
                                           || (x.cardShameType == CardShameType.Buff)
                                           || (x.cardShameType == CardShameType.Debuff)).currentShame;
        int cost = dataList.Find(x => x.cardShameType == CardShameType.Cost).currentShame;
        int targetCount = dataList.Find(x => x.cardShameType == CardShameType.Range).currentShame;

        int afterShame = dataList.Find(x =>   (x.cardShameType == CardShameType.Damage)
                                           || (x.cardShameType == CardShameType.Buff)
                                           || (x.cardShameType == CardShameType.Debuff)).afterShame;
        int aftercost = dataList.Find(x => x.cardShameType == CardShameType.Cost).afterShame;
        int aftertargetCount = dataList.Find(x => x.cardShameType == CardShameType.Range).afterShame;

        return ((currentShame * targetCount / cost), (afterShame * aftertargetCount / aftercost));
    }

    public void SetGookBapShame(List<CardShameData> currentDataList)
    {
        (float, float) gookPower = GetGookBapShame(currentDataList);

        _gookBapLevelText.text = 
        $"{gookPower.Item1} -> <color=#37B1FF>{gookPower.Item2}<size=25> (+{gookPower.Item2 - gookPower.Item1})</size></color>";
    }

    public override void SetCardInfo(CardShameElementSO shameData, CardInfo cardInfo, int combineLevel)
    {
        combineLevel = Mathf.Clamp(combineLevel - 1, 0, 5);
        List<CardShameData> dataList = shameData.cardShameDataList[combineLevel].list;

        SetGookBapShame(dataList);

        _cardShameTrm.Clear();
        foreach (CardShameData data in dataList)
        {
            CardShameElement cse = Instantiate(_cardShameElement, _cardShameTrm);
            cse.SetShame(GetShameDataGroup(data));
        }
    }
}
