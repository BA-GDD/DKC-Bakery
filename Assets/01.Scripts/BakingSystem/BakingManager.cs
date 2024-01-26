using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Flags]
public enum IngredientType
{
    None = 0,       // 없음 (체크용)
    Base = 1,       // 베이스
    Liquid = 2,     // 액체류 - 물, 우유, 달걀
    Leaven = 4,     // 효모
    Butterfat = 8,  // 유지방
    Sugars = 16, // 당류
}

public class BakingManager : MonoSingleton<BakingManager>
{
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
        usedIngredientStash = new UsedIngredientStash(_usedIngredientParent);
        breadDictionary = new Dictionary<string, ItemDataBreadSO>();
        for(int i = 0; i < _breadList.Count; ++i)
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
        if(Keyboard.current.hKey.wasPressedThisFrame)
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
        if(ingredientSO != null)
        {
            onRemoveUsedIngredientTrigger?.Invoke(ingredientSO.itemIndex);
        }
        usedIngredientStash.RemoveItem(item);
        UpdateSlotUI();
    }

    public void SetBakingUI(bool isOpen)
    {
        _bakingUI.SetActive(isOpen);
    }

    public void BakeBread()
    {
        Debug.Log(usedIngredientStash.usedIngredDictionary.Count);
        if(usedIngredientStash.usedIngredDictionary.Count >= 5)
        {
            string[] names = new string[5];
            for(int i = 0; i < 5; ++i)
            {
                names[i] = usedIngredientStash.usedIngredientStash[i].itemDataSO.itemName;
            }
            ItemDataBreadSO returnBread = _recipeTable.Bake(names);
            if(returnBread != null)
            {
                Inventory.Instance.AddItem(returnBread);
                usedIngredientStash.RemoveAllItem();
                usedIngredientStash.UpdateSlotUI();
            }
        }
    }
}