using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUpgrade : MonoBehaviour
{
    // 카드 정보(인벤토리) 불러오기
    // 재화 정보 불러오기
    // 강화가 가능한 카드인지.

    public int debugToUpgradeMoney = 100;
    public int currentMoney;

    [SerializeField] private CardUpgradeTableSO cardTable;

    private void Awake()
    {
        //나중에~
        cardTable.SetDic();
    }

    public bool IsAbleToUpgrade(CardInfo cardInfo)
    {
        if (cardInfo == null) return false;
        print(cardInfo.CardName);
        print(cardInfo.CardLevel);
        return cardInfo.CardLevel < 5 && currentMoney >= debugToUpgradeMoney && cardTable.findCardInfo.ContainsKey(cardInfo.CardName);
    }

    public void UpgradeCard(CardInfo cardInfo)
    {
        cardInfo.CardLevel++;
        currentMoney -= debugToUpgradeMoney;

        //DataManager.Instance.SaveData();

        //시트에서 불러와서 넣기
        cardTable.findCardInfo[cardInfo.CardName].SetList();
        cardInfo.CardAttackDamage = float.Parse(cardTable.findCardInfo[cardInfo.CardName].genDatas[0].str[cardInfo.CardLevel]);
    }

    public void DowngradeCard(CardInfo cardInfo)
    {
        cardInfo.CardLevel--;
        cardTable.findCardInfo[cardInfo.CardName].SetList();
        cardInfo.CardAttackDamage = float.Parse(cardTable.findCardInfo[cardInfo.CardName].genDatas[0].str[cardInfo.CardLevel]);
    }
}
