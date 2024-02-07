using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectIngredientBox : MonoBehaviour
{
    [SerializeField] private IngredientType _markIngredientType;
    public IngredientType IngredientType => _markIngredientType;
    private ItemDataIngredientSO _itemInfo;
    [SerializeField] private Image _iconImg;

    public void SelectIngredient(ItemDataIngredientSO itemInfo)
    {
        _itemInfo = itemInfo;
        _iconImg.sprite = itemInfo.itemIcon;

        _iconImg.enabled = true;
    }

    public void RemoveIngredient()
    {
        _itemInfo = null;
        _iconImg.enabled = false;
    }
}
