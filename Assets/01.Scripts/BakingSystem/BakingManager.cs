using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum IngredientType
{
    None = 0,       // ���� (üũ��)
    Base = 1,       // ���̽�
    Liquid = 2,     // ��ü�� - ��, ����, �ް�
    Leaven = 3,     // ȿ��
    Butterfat = 4,  // ������
    Sugars = 5,     // ���
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