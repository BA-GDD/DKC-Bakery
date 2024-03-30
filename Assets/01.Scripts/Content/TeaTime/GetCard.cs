using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCard : MonoBehaviour
{
    private CardBase _toGetCard;

    public void GetCakeInfo(ItemDataBreadSO cakeInfo)
    {
        _toGetCard = cakeInfo.ToGetCardBase;
    }
}
