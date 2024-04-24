using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public struct NeedIngredent
{
    public ItemDataIngredientSO itemData { get; set; }
    public Image visual;
    public TextMeshProUGUI countText;
}

public class RecipeElement : MonoBehaviour
{
    [SerializeField] private Vector2 _crookedAngleRange;
    public CakeData ThisCakeData { get; set; }

    [Header("������")]
    [SerializeField] private TextMeshProUGUI _cakeNameText;
    [SerializeField] private Image _cakeVisual;
    [SerializeField] private NeedIngredent[] _needIngredientArr = new NeedIngredent[3];
    [SerializeField] private FavoritesMark _favoriteMark;

    public void SetCakeInfo(ItemDataBreadSO cakeInfo)
    {
        _cakeNameText.text = cakeInfo.itemName;
        _cakeVisual.sprite = cakeInfo.itemIcon;

        _cakeVisual.transform.parent.rotation =
        Quaternion.Euler(0, 0, UnityEngine.Random.Range(_crookedAngleRange.x, _crookedAngleRange.y));

        _favoriteMark.SetState(this);

        ItemDataIngredientSO[] ingDatas = 
        BakingManager.Instance.GetIngredientDatasByCakeName(cakeInfo.itemName);

        for(int i = 0; i < _needIngredientArr.Length; i++)
        {
            _needIngredientArr[i].itemData = ingDatas[i];

            _needIngredientArr[i].visual.sprite = ingDatas[i].itemIcon;
            _needIngredientArr[i].countText.text = "";
        }
    }
}
