using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public abstract class FilterTab : MonoBehaviour
{
    [SerializeField] private Button _tapBtn;
    protected Image BtnImg => _tapBtn.image;
    public Button TapBtn
    {
        get
        {
            return _tapBtn;
        }
    }
    [SerializeField] private IngredientType _ingredientType;
    public IngredientType GetIngredientType => _ingredientType;
    [SerializeField] protected TextMeshProUGUI _tapLabel;
    [SerializeField] protected float _labelBlurValue = 0.8f;
    [SerializeField] protected float _easingTime = .15f;

    public abstract void ActiveTab(bool isActive);
}
