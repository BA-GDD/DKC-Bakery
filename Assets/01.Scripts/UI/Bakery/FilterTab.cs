using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FilterTab : MonoBehaviour
{
    [SerializeField] private Button _tapBtn;
    private Image BtnImg => _tapBtn.image;
    public Button TapBtn
    {
        get
        {
            return _tapBtn;
        }
    }
    [SerializeField] private IngredientType _ingredientType;
    public IngredientType GetIngredientType => _ingredientType;
    [SerializeField] private TextMeshProUGUI _tapLabel;

    private readonly int _normalX = -744;
    private readonly float _labelBlurValue = 0.8f;
    private readonly int _addValue = -16;
    private readonly float _easingTime = .15f;

    public void ActiveTab(bool isActive)
    {
        if(isActive)
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
