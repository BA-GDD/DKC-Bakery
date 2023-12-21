using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum IngredientType
{
    Base = 0,       // 베이스
    Liquid = 1,     // 액체류 - 물, 우유, 달걀
    Leaven = 2,     // 효모
    Butterfat = 3,  // 유지방
    Sugars = 4,     // 당류
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