using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Inventory : MonoSingleton<Inventory>
{
    public IngredientStash ingredientStash;
    public BreadStash breadStash;

    public Transform IngredientParent => _ingredientParent;
    [Header("ParentTrms")]
    [SerializeField] private Transform _ingredientParent; 
    [SerializeField] private Transform _breadParent;
    public ExpansionList<ItemDataIngredientSO> GetIngredientInThisBattle { get; set; } = new ExpansionList<ItemDataIngredientSO>();

    [Header("Events")]
    public UnityEvent<int> onRemoveBreadTrigger; 
    public UnityEvent<int> onRemoveIngredientTrigger; 

    private void Awake()
    {
        ingredientStash = new IngredientStash(_ingredientParent);
        breadStash = new BreadStash(_breadParent);

        SceneManager.activeSceneChanged += HandleClearGetIngList;
        GetIngredientInThisBattle.ListAdded += HandleGetItem;
    }

    private void HandleGetItem(object sender, EventArgs e)
    {
        Debug.Log(sender);
    }

    private void HandleClearGetIngList(Scene arg0, Scene arg1)
    {
        GetIngredientInThisBattle.Clear();
    }

    private void Start()
    {
        UpdateSlotUI();
    }
    
    public void UpdateSlotUI() 
    {
        ingredientStash.UpdateSlotUI();
        breadStash.UpdateSlotUI();
    }
    public void AddItem(ItemDataSO item, int count = 0)
    { 
        if (item.itemType == ItemType.Bread)
        {
            if (breadStash.CanAddItem(item))
            {
                breadStash.AddItem(item);
            }
        }
        else if (item.itemType == ItemType.Ingredient)
        {
            Debug.Log("itemType is Ingredient");
            if (ingredientStash.CanAddItem(item))
            {
                Debug.Log("Can Add Item");
                ingredientStash.AddItem(item, count);
            }
        }
        UpdateSlotUI();
    }
    public void RemoveItem(ItemDataSO item, int count = 1)
    {
        if (item.itemType == ItemType.Bread)
        {
            ItemDataBreadSO breadSO = ((ItemDataBreadSO)item);
            breadStash.RemoveItem(item, count);
        }
        else if (item.itemType == ItemType.Ingredient)
        {
            ItemDataIngredientSO ingredientSO = ((ItemDataIngredientSO)item);
            if (ingredientSO != null)
            {
                onRemoveIngredientTrigger.Invoke(ingredientSO.itemIndex);
            }
            ingredientStash.RemoveItem(item, count);
        }
        UpdateSlotUI();
    }
}
