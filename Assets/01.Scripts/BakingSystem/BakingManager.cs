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
    public IngredientStash useIngredientStash;

    [Header("PatrentTrm")]
    [SerializeField] private Transform _useIngredientParent;

    [Header("Events")]
    public UnityEvent onBakeBreadTrigger;

    private void Awake()
    {
        useIngredientStash = new IngredientStash(_useIngredientParent);
    }

    private void Start()
    {
        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        useIngredientStash.UpdateSlotUI();
    }

    public void AddItem(ItemDataIngredientSO item, int count = 1)
    {
        
    }
}