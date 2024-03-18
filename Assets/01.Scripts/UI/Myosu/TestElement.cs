using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestElement : PoolableMono, 
                           IPointerEnterHandler,
                           IPointerExitHandler,
                           IPointerClickHandler
{
    [Header("참조 값")]
    [SerializeField] private TextMeshProUGUI _infoText;
    public MyosuTestInfo TestInfo { get; set; }

    [Header("마스크")]
    [SerializeField] private RectTransform _maskTrm;
    [SerializeField] private TextMeshProUGUI _selectText;

    public int TestIdx { get; set; }

    private Tween _maskTween;
    private Tween _textEmphasisTween;
    private bool _isSelected;
    private bool IsSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
        }
    }

    public override void Init()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _maskTween.Kill();
        _maskTween = _maskTrm.DOLocalMoveX(0, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _maskTween.Kill();
        _maskTween = _maskTrm.DOLocalMoveX(700, 0.2f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsSelected = !IsSelected;

        _textEmphasisTween.Kill();
        _textEmphasisTween = _selectText.transform.DOScale(Vector3.one * 1.1f, 0.1f).
        OnComplete(()=> _selectText.transform.DOScale(Vector3.one, 0.1f));

        _selectText.text = IsSelected ? "SELECT?" : "SELECTED!";
    }
}
