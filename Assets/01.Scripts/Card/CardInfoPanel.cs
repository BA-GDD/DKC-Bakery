using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfoPanel : PoolableMono
{
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private TextMeshProUGUI _skillInfoText;

    public void SetInfo(CardInfo info)
    {
        _skillNameText.text = info.AbillityName;
        _skillInfoText.text = info.AbillityInfo;
    }

    public override void Init()
    {

    }
}
