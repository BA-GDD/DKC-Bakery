using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryFiterTab : FilterTab
{
    public override void ActiveTab(bool isActive)
    {
        if(isActive)
        {
            BtnImg.DOColor(Color.white, _easingTime);
            _tapLabel.DOColor(Color.white, _easingTime);
        }
        else
        {
            Color unActiveColor = Color.white * _labelBlurValue;
            unActiveColor.a = 1;

            BtnImg.DOColor(unActiveColor, _easingTime);
            _tapLabel.DOColor(unActiveColor, _easingTime);
        }
    }
}
