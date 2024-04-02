using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterTabGroup : MonoBehaviour
{
    private List<ItemElement> _itemElementList = new List<ItemElement>();
    [SerializeField] private RectTransform _contentTrm;
    [SerializeField] private Transform _popUpParent;
    private FilterTab _currentFilterType;
    public FilterTab CurrentFilterTab => _currentFilterType;
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
        bool isOtherFilter = filterTab != _currentFilterType;
        Stack<ItemDataIngredientSO> s = new();
        foreach (ItemElement item in _itemElementList)
        {
            PoolManager.Instance.Push(item);
            s.Push(item.IngredientSO);
        }
        _itemElementList.Clear();
        
        _currentFilterType.ActiveTab(false);
        filterTab.ActiveTab(true);

        int matchItemCount = 0;
        Inventory.Instance.ingredientStash.stash.Sort();
        foreach (InventoryItem item in Inventory.Instance.ingredientStash.stash)
        {
            ItemDataIngredientSO ingso = item.itemDataSO as ItemDataIngredientSO;
            if ((filterTab.GetIngredientType & ingso.ingredientType) == ingso.ingredientType)
            {
                matchItemCount++;

                ItemElement ie = PoolManager.Instance.Pop(PoolingType.IngredientItemElement) as ItemElement;
                ie.IngredientSO = isOtherFilter ? ingso : s.Pop();
                ie.CountText = item.stackSize.ToString();
                ie.PopUpPanelParent = _popUpParent;
                ie.transform.SetParent(_contentTrm);
                ie.transform.localScale = Vector3.one;
                ie.transform.SetAsFirstSibling();
                _itemElementList.Add(ie);
            }
        }
        _contentTrm.sizeDelta = new Vector2(0, ((matchItemCount % 4) + 1) * 140);
        _currentFilterType = filterTab;
    }
}
