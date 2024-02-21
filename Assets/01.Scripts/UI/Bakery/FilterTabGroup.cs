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

    [SerializeField] private ItemDataSO[] _sampleitemData;

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
        for(int i = 0; i < _sampleitemData.Length; i++)
        {
            Inventory.Instance.AddItem(_sampleitemData[i], 1);
        }
        
        FilteringItem(_filterTabArr[0]);
    }

    public void FilteringItem(FilterTab filterTab)
    {
        _currentFilterType.ActiveTab(false);
        filterTab.ActiveTab(true);

        foreach (ItemElement item in _itemElementList)
        {
            PoolManager.Instance.Push(item);
        }
        _itemElementList.Clear();

        int matchItemCount = 0;
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

                _itemElementList.Add(ie);
            }
        }
        _contentTrm.sizeDelta = new Vector2(0, ((matchItemCount % 4) + 1) * 140);
        _currentFilterType = filterTab;
    }
}
