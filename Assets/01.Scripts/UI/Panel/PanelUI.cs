using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[Serializable]
public class PanelUI : MonoBehaviour
{
    public bool useBlackPanel = false;
    [HideInInspector] public Image blackPanel;
    [HideInInspector] public float easingTime = 0.2f;
    [HideInInspector] public float endOfAlpha = 0.3f;

    public void FadePanel(bool isActive)
    {
        if(!useBlackPanel)
        {
            Debug.LogError("Error! This panel not use 'BlackPanel'!!\nIf you want to use 'BlackPanel', you must check 'useBlackPanel'(type : Boolean)");
            return;
        }

        blackPanel.DOFade(endOfAlpha * MaestrOffice.ConvertBoolToInt(isActive), easingTime);
    }

    public void FadePanel(bool isActive, Action callBack)
    {
        if (!useBlackPanel)
        {
            Debug.LogError("Error! This panel not use 'BlackPanel'!!\nIf you want to use 'BlackPanel', you must check 'useBlackPanel'(type : Boolean)");
            return;
        }

        blackPanel.DOFade(endOfAlpha * MaestrOffice.ConvertBoolToInt(isActive), easingTime).OnComplete(()=> callBack());
    }
}
