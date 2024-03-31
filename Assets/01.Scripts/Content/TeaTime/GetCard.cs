using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCard : MonoBehaviour
{
    private CanUseCardData _canUseCardData = new CanUseCardData();
    private const string CardDataKey = "GetCardData";
    private CardBase _toGetCard;

    public void GetCakeInfo(ItemDataBreadSO cakeInfo)
    {
        _toGetCard = cakeInfo.ToGetCardBase;

        if(DataManager.Instance.IsHaveData(CardDataKey))
        {
            _canUseCardData = DataManager.Instance.LoadData<CanUseCardData>(CardDataKey);
        }

        _canUseCardData.CanUseCardsList.Add(_toGetCard.CardInfo.CardName);
        DataManager.Instance.SaveData(_canUseCardData, CardDataKey);
    }
}
