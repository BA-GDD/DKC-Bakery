using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum IngredientType
{
    Base = 0,       // ���̽�
    Liquid = 1,     // ��ü�� - ��, ����, �ް�
    Leaven = 2,     // ȿ��
    Butterfat = 3,  // ������
    Sugars = 4,     // ���
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
    }

    public void SetBakingUI(bool isOpen)
    {
        _bakingUI.SetActive(isOpen);
    }
}