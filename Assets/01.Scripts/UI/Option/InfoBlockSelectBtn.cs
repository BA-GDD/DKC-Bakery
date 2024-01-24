using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InfoBlockSelectBtn : MonoBehaviour
{
    [Header("셋팅값")]
    [SerializeField] private TextMeshProUGUI _selectText;
    [SerializeField] private Image _btnImg;
    [SerializeField] private CanvasGroup _markBlock;
    [SerializeField] private bool _clickThisPanel;
    public bool ClickThisPanel
    {
        get
        {
            return _clickThisPanel;
        }
        set
        {
            _clickThisPanel = value;
            HandlePointerClickBtn(_clickThisPanel);
            HandleOutPointerBtn();
        }
    }

    [Header("수치")]
    [SerializeField] private float _selectMoveValue;
    [SerializeField] private float _colorBlurValue;
    [SerializeField] private float _easingTime;

    private Color _blurColor;
    private readonly float _normalPosX = -428;

    private void Start()
    {
        _blurColor = Color.white * _colorBlurValue;
        _blurColor.a = 1;

        if(_clickThisPanel)
        {
            HandlePointerClickBtn(_clickThisPanel);
        }
        else
        {
            HandleOutPointerBtn();
        }
    }

    public void HandleOnPointerBtn()
    {
        if (_selectText.color == Color.white) return;

        _selectText.DOColor(Color.white, _easingTime);
    }

    public void HandleOutPointerBtn()
    {
        if (_clickThisPanel) return;

        _selectText.DOColor(_blurColor, _easingTime);
    }

    private void HandlePointerClickBtn(bool isSelect)
    {
        _markBlock.alpha = MaestrOffice.ConvertBoolToInt(isSelect);
        _markBlock.blocksRaycasts = isSelect;
        _markBlock.interactable = isSelect;

        float movingPosX = isSelect ? _normalPosX + _selectMoveValue : _normalPosX;
        Color btnColor = isSelect ? Color.white : _blurColor;

        _btnImg.transform.DOLocalMoveX(movingPosX, _easingTime);
        _btnImg.DOColor(btnColor, _easingTime);
    }
}
