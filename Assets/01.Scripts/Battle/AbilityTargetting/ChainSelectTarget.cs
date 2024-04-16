using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainSelectTarget : MonoBehaviour
{
    [SerializeField] private RectTransform _chainMaskTrm;
    [SerializeField] private Image[] _chinImg;

    private void SetFade()
    {
        foreach (var img in _chinImg)
        {
            img.DOFade(0.5f, 0.2f);
        }
    }

    public void SetMark()
    {
        float targetValue = Mathf.Abs(_chainMaskTrm.localPosition.x * 2);
        DOTween.To(() => 0, x => _chainMaskTrm.sizeDelta = new Vector2(x, _chainMaskTrm.sizeDelta.y), targetValue, 0.5f).SetEase(Ease.InQuart);

        int randZ = Random.Range(-360, 360);
        transform.rotation = Quaternion.Euler(0, 0, randZ);
        SetFade();
    }
}
