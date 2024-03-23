using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TitleButton : MonoBehaviour
{
    [SerializeField] private RectTransform _labelRect;
    [SerializeField] private Image _labelImg;

    [SerializeField] private float _startXValue;
    [SerializeField] private float _endXValue;
    [SerializeField] private Color _normalColor;

    private Tween _appearTween;
    private Sequence _disappearSeq;

    public void HobberEvent()
    {
        _disappearSeq.Kill();

        _labelImg.enabled = true;
        _labelRect.localPosition = new Vector3(_startXValue, _labelRect.transform.localPosition.y);
        _labelRect.localScale = Vector3.one;
        _labelImg.color = _normalColor;

        _appearTween = _labelRect.DOLocalMoveX(_endXValue, 0.2f);
    }
    public void UnHobberEvent()
    {
        _appearTween.Kill();

        _disappearSeq = DOTween.Sequence();
        _disappearSeq.Append(_labelRect.DOScaleY(0, 0.4f));
        _disappearSeq.Join(_labelImg.DOFade(0, 0.4f));
        _disappearSeq.AppendCallback(()=> _labelImg.enabled = false);
    }
    public abstract void PressEvent();
}
