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

    public ExpansionList<ItemDataIngredientSO> GetIngredientInThisBattle { get; set; } = 
       new ExpansionList<ItemDataIngredientSO>();

    private void Awake()
    {
        ingredientStash = new IngredientStash(transform);
        breadStash = new BreadStash(transform);

        SceneManager.activeSceneChanged += HandleClearGetIngList;
    }

    private void HandleClearGetIngList(Scene arg0, Scene arg1)
    {
        GetIngredientInThisBattle.Clear();
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
            //Debug.Log("itemType is Ingredient");
            if (ingredientStash.CanAddItem(item))
            {
                //Debug.Log("Can Add Item");
                ingredientStash.AddItem(item, count);
            }
        }
    }
    public void RemoveItem(ItemDataSO item, int count = 1)
    {
        if (item.itemType == ItemType.Bread)
        {
            breadStash.RemoveItem(item, count);
        }
        else if (item.itemType == ItemType.Ingredient)
        {
            ItemDataIngredientSO ingredientSO = ((ItemDataIngredientSO)item);
            ingredientStash.RemoveItem(item, count);
        }
    }
}
