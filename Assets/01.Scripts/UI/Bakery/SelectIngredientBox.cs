using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectIngredientBox : MonoBehaviour, IPointerClickHandler
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
        _iconImg.enabled = false;
        BakingManager.Instance.RemoveItem(_itemInfo);
        Inventory.Instance.AddItem(_itemInfo);
        _itemInfo.isUsed = false;
        BakingManager.Instance.FilterTabGroup.FilteringItem(BakingManager.Instance.FilterTabGroup.CurrentFilterTab);
        _itemInfo = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BakingManager.Instance.CookingBox.RemoveSelectIngredientInfo(IngredientType);
    }
}