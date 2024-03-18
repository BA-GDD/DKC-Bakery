using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyosuPanel : MonoBehaviour
{
    public MyosuTestInfo MyosuTestInfo { get; set; }
    private Tween _setupTween;

    [Header("참조 값")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Image _myosuVisual;

    [Header("셋팅 값")]
    [SerializeField] private float _normalXValue;
    [SerializeField] private float _hideXValue;

    public void SetUpPanel(bool isSetUp)
    {
        float xValue = isSetUp ? _normalXValue : _hideXValue;

        _setupTween.Kill();
        _setupTween = transform.DOLocalMoveX(xValue, 0.3f).SetEase(Ease.InBack);
    }
}
