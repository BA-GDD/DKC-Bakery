using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterTabGroup : MonoBehaviour
{
    private List<ItemElement> _itemElementList = new List<ItemElement>();
    private List<InventoryItem> _invenItems = new List<InventoryItem>();
    [SerializeField] private RectTransform _contentTrm;
    [SerializeField] private Transform _popUpParent;
    private FilterTab _currentFilterType;
    public FilterTab CurrentFilterTab => _currentFilterType;
    private FilterTab[] _filterTabArr;

    private void Awake()
    {
        _filterTabArr = GetComponentsInChildren<FilterTab>();
        foreach (FilterTab ft in _filterTabArr)
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
        bool isOtherFilter = filterTab != _currentFilterType;
        if (isOtherFilter)
        {
            foreach (ItemElement item in _itemElementList)
                PoolManager.Instance.Push(item);
            _itemElementList.Clear();
            _invenItems.Clear();
        }

        _currentFilterType.ActiveTab(false);
        filterTab.ActiveTab(true);

        int matchItemCount = 0;
        Inventory.Instance.ingredientStash.stash.Sort();
        if (isOtherFilter)
        {
            foreach (InventoryItem item in Inventory.Instance.ingredientStash.stash)
            {
                ItemDataIngredientSO ingso = item.itemDataSO as ItemDataIngredientSO;
                if ((filterTab.GetIngredientType & ingso.ingredientType) == ingso.ingredientType)
                {
                    matchItemCount++;
                    ItemElement ie = PoolManager.Instance.Pop(PoolingType.IngredientItemElement) as ItemElement;
                    ie.IngredientSO = ingso;
                    ie.CountText = item.stackSize.ToString();
                    ie.PopUpPanelParent = _popUpParent;
                    ie.transform.SetParent(_contentTrm);
                    ie.transform.localScale = Vector3.one;
                    ie.ActiveUpdateIngredientUseMask();
                    _itemElementList.Add(ie);
                    _invenItems.Add(item);
                }
            }
        }
        else
        {
            for (int i = 0; i < _invenItems.Count; i++)
            {
                _itemElementList[i].CountText = _invenItems[i].stackSize.ToString();
                if (!Inventory.Instance.ingredientStash.stash.Contains(_invenItems[i]))
                {
                    PoolManager.Instance.Push(_itemElementList[i]);
                    _invenItems.RemoveAt(i);
                    _itemElementList.RemoveAt(i);
                    i--;
                }
            }
            foreach (var item in _itemElementList)
            {
                item.ActiveUpdateIngredientUseMask();
            }
        }
        _contentTrm.sizeDelta = new Vector2(0, ((matchItemCount % 4) + 1) * 140);
        _currentFilterType = filterTab;
    }
}
