using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Flags]
public enum IngredientType
{
    None = 0,       // 绝澜 (眉农侩)
    Core = 1,       // 海捞胶
    Trace = 2,     // 如利
    Subjectivity = 4,     // 林包
    Else = 8
}

public class BakingManager : MonoSingleton<BakingManager>
{
    private CookingBox _cookingBox;
    public CookingBox CookingBox
    {
        get
        {
            if (_cookingBox != null)
                return _cookingBox;
            _cookingBox = FindObjectOfType<CookingBox>();
            return _cookingBox;
        }
    }

    private FilterTabGroup _fiterTabGroup;
    public FilterTabGroup FilterTabGroup
    {
        get
        {
            if (_fiterTabGroup != null)
                return _fiterTabGroup;
            _fiterTabGroup = FindObjectOfType<FilterTabGroup>();
            return _fiterTabGroup;
        }
    }

    public UsedIngredientStash usedIngredientStash;
    public bool isOpen = false;

    [SerializeField] private GameObject _bakingUI;

    [Header("ParentTrm")]
    [SerializeField] private Transform _usedIngredientParent;

    [Header("Events")]
    public UnityEvent<int> onRemoveUsedIngredientTrigger;

    [SerializeField]
    private BreadRecipeTable _recipeTable;

    public Dictionary<string, ItemDataBreadSO> breadDictionary;
    [SerializeField]
    private List<ItemDataBreadSO> _breadList;

    private void Awake()
    {
        if (_usedIngredientParent == null)
        {
            _usedIngredientParent = Inventory.Instance.IngredientParent;
        }

        usedIngredientStash = new UsedIngredientStash(_usedIngredientParent);
        breadDictionary = new Dictionary<string, ItemDataBreadSO>();
        for (int i = 0; i < _breadList.Count; ++i)
        {
            breadDictionary.Add(_breadList[i].itemName, _breadList[i]);
        }
    }

    private void Start()
    {
        SetBakingUI(isOpen);
        UpdateSlotUI();
    }

    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            isOpen = !isOpen;
            SetBakingUI(isOpen);
        }
    }

    public void UpdateSlotUI()
    {
        usedIngredientStash.UpdateSlotUI();
    }

    public void AddItem(ItemDataSO item)
    {
        if (usedIngredientStash.CanAddItem(item))
        {
            usedIngredientStash.AddItem(item);
        }
        UpdateSlotUI();
    }

    public void RemoveItem(ItemDataSO item)
    {
        ItemDataIngredientSO ingredientSO = ((ItemDataIngredientSO)item);
        if (ingredientSO != null)
        {
            onRemoveUsedIngredientTrigger?.Invoke(ingredientSO.itemIndex);
        }
        usedIngredientStash.RemoveItem(item);
        UpdateSlotUI();
    }

    public void SetBakingUI(bool isOpen)
    {
        //_bakingUI.SetActive(isOpen);
    }

    public bool CanBake()
    {
        return usedIngredientStash.usedIngredDictionary.Count >= 3;
    }

    public ItemDataBreadSO BakeBread()
    {
        if (!CanBake())
        {
            Debug.LogError("Plz Check Can Bake");
            return null;
        }

        string[] names = new string[5];
        for (int i = 0; i < 3; ++i)
        {
            int result = (int)Mathf.Pow(2, i);
            names[i] = usedIngredientStash.usedIngredientStash[result].itemDataSO.itemName;
        }
        ItemDataBreadSO returnBread = _recipeTable.Bake(names);
        if (returnBread != null)
        {
            Inventory.Instance.AddItem(returnBread);
            usedIngredientStash.RemoveAllItem();
            usedIngredientStash.UpdateSlotUI();
        }

        FilterTabGroup.FilteringItem(FilterTabGroup.CurrentFilterTab);
        return returnBread;
    }
}