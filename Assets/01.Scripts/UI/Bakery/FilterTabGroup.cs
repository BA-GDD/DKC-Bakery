using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;

public class FilterTabGroup : MonoBehaviour
{
    [SerializeField] private ItemElement _bakeryItemElementPrefab;
    private List<ItemElement> _itemElementList = new List<ItemElement>();
    [SerializeField] private Transform _contentTrm;
    private IngredientType _currentFilterType = IngredientType.None;

    private void Awake()
    {
        FilterTab[] tabArr = GetComponentsInChildren<FilterTab>();
        foreach(FilterTab ft in tabArr)
        {
            ft.TapBtn.onClick.AddListener(() => FilteringItem(ft.GetIngredientType));
        }
    }

    public void FilteringItem(IngredientType filterType)
    {
        if (_currentFilterType == filterType) return;

        foreach (ItemElement item in  _itemElementList)
        {
            PoolManager.Instance.Push(item);
        }

        _itemElementList.Clear();

        foreach(InventoryItem item in Inventory.Instance.ingredientStash.stash)
        {
            ItemDataIngredientSO ingso = item.itemDataSO as ItemDataIngredientSO;

            if(filterType == ingso.ingredientType)
            {
                ItemElement ie = PoolManager.Instance.Pop(PoolingType.IngredientItemElement) as ItemElement;
                ie.ItemImg = ingso.itemIcon;
                ie.CountText = item.stackSize.ToString();
                ie.transform.SetParent(_contentTrm);

                _itemElementList.Add(ie);
            }
        }
    }
}
