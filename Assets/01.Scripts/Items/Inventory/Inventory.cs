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
    
    [Header("Events")]
    public UnityEvent<int> onRemoveBreadTrigger; 
    public UnityEvent<int> onRemoveIngredientTrigger; 

    public ExpansionList<ItemDataIngredientSO> GetIngredinentsInThisBattle { get; set; } = new ExpansionList<ItemDataIngredientSO>();
    

    private void Awake()
    {
        ingredientStash = new IngredientStash(_ingredientParent);
        breadStash = new BreadStash(_breadParent);

        GetIngredinentsInThisBattle.ListAdded += HandleGetItem;
        SceneManager.activeSceneChanged += (Scene a, Scene v) => GetIngredinentsInThisBattle.Clear();
    }

    private void HandleGetItem(object sender, EventArgs e)
    {
        Debug.Log(sender);
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
            if(breadSO!= null)
            {
                onRemoveBreadTrigger.Invoke(breadSO.hogamdo);
            }
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
