using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CardUpgradeInfoStruct
{
    public string Name;
    public CardUpgradeInfo cardUpgradeInfo;
}

[CreateAssetMenu(menuName ="SO/CardUpgrade/table")]
public class CardUpgradeTableSO : ScriptableObject
{
    public List<CardUpgradeInfoStruct> cardUpgradeInfos = new List<CardUpgradeInfoStruct>();
    private Dictionary<string, CardUpgradeInfo> findCardInfo = new Dictionary<string, CardUpgradeInfo>();

    public void SetDic()
    {
        for(int i = 0; i < cardUpgradeInfos.Count; i++)
        {
            findCardInfo.Add(cardUpgradeInfos[i].Name, cardUpgradeInfos[i].cardUpgradeInfo);
        }
    }
}
