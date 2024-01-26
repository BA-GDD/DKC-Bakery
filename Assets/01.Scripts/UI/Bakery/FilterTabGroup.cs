using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterTabGroup : MonoBehaviour
{
    [SerializeField] private ItemElement _bakeryItemElementPrefab;
    private List<ItemElement> _itemElementList = new List<ItemElement>();
    [SerializeField] private Transform _contentTrm;
    private FilterTab _currentFilterType;
    private FilterTab[] _filterTabArr;

    private void Awake()
    {
        _filterTabArr = GetComponentsInChildren<FilterTab>();
        foreach(FilterTab ft in _filterTabArr)
        {
            ft.TapBtn.onClick.AddListener(() => FilteringItem(ft));
        }
        _currentFilterType = _filterTabArr[1];
    }

    private void Start()
    {
        FilteringItem(_filterTabArr[0]);
    }

    public void FilteringItem(FilterTab filterTab)
    {
        if (_currentFilterType.GetIngredientType == filterTab.GetIngredientType) return;

        _currentFilterType.ActiveTab(false);
        filterTab.ActiveTab(true);

        //foreach (ItemElement item in  _itemElementList)
        //{
        //    PoolManager.Instance.Push(item);
        //}
        //_itemElementList.Clear();
        //foreach(InventoryItem item in Inventory.Instance.ingredientStash.stash)
        //{
        //    ItemDataIngredientSO ingso = item.itemDataSO as ItemDataIngredientSO;

        //    if(filterTab.GetIngredientType == ingso.ingredientType)
        //    {
        //        ItemElement ie = PoolManager.Instance.Pop(PoolingType.IngredientItemElement) as ItemElement;
        //        ie.ItemImg = ingso.itemIcon;
        //        ie.CountText = item.stackSize.ToString();
        //        ie.transform.SetParent(_contentTrm);

        //        _itemElementList.Add(ie);
        //    }
        //}
        _currentFilterType = filterTab;
    }
}
