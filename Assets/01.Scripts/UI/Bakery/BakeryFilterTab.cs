using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BakeryFilterTab : FilterTab
{
    [SerializeField] private int _normalX = -744;
    [SerializeField] private int _addValue = -16;

    public override void ActiveTab(bool isActive)
    {
        if (isActive)
        {
            transform.DOLocalMoveX(_normalX + _addValue, _easingTime);
            BtnImg.DOColor(Color.white, _easingTime);
            _tapLabel.DOColor(Color.white, _easingTime);
        }
        else
        {
            Color unActiveColor = Color.white * _labelBlurValue;
            unActiveColor.a = 1;

            transform.DOLocalMoveX(_normalX, _easingTime);
            BtnImg.DOColor(unActiveColor, _easingTime);
            _tapLabel.DOColor(unActiveColor, _easingTime);
        }
    }
}
