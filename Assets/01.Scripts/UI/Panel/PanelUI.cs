using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


[Serializable]
public class PanelUI : MonoBehaviour
{
    public bool useBlackPanel = false;
    [HideInInspector] public Image blackPanel;
    [HideInInspector] public float easingTime = 0.2f;
    [HideInInspector] public float endOfAlpha = 0.3f;

    public void FadePanel()
    {
        if(!useBlackPanel)
        {
            Debug.LogError("Error! This panel not use 'BlackPanel'!!\nIf you want to use 'BlackPanel', you must check 'useBlackPanel'(type : Boolean)");
            return;
        }


    }
}
