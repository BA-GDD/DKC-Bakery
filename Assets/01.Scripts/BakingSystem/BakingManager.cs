using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum IngredientType
{
    None = 0,       // 없음 (체크용)
    Base = 1,       // 베이스
    Liquid = 2,     // 액체류 - 물, 우유, 달걀
    Leaven = 3,     // 효모
    Butterfat = 4,  // 유지방
    Sugars = 5,     // 당류
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

    private void Awake()
    {
        usedIngredientStash = new UsedIngredientStash(_usedIngredientParent);
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
}