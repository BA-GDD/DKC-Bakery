using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class Stage01 : Stage
{
   /* [SerializeField]
    private SpriteRenderer[] backgroundSpriteRenderer = null;
    private Color endColor = new Color(1, 1, 1, 0);
    private Color defaultColor = new Color(1, 1, 1, 1);
    private Action callback;


    public override void PhaseCleared()
    {
        base.PhaseCleared();
        callback = _stageInfo[curPhase] == GameManager.Instance.PlayerTrm ?
                () =>
                {
                    foreach(var t in backgroundSpriteRenderer)
                    {
                        t.DOColor(endColor, .2f);
                    }
                } :
                () =>
                {
                    foreach (var t in backgroundSpriteRenderer)
                    {
                        t.DOColor(defaultColor, .2f);
                    }
                };
        callback?.Invoke();
    }*/
}
