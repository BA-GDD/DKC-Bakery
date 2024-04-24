using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum IngredientType
{
    Core,       // 海捞胶
    Trace,     // 如利
    Subjectivity,     // 林包
}

public class BakingManager : MonoSingleton<BakingManager>
{
    public UsedIngredientStash usedIngredientStash;

    [SerializeField] private BreadRecipeTable _recipeTable;

    public Dictionary<string, ItemDataBreadSO> BreadDictionary { get; private set; }
    [SerializeField] private List<ItemDataBreadSO> _breadList = new();

    public Dictionary<string, ItemDataIngredientSO> IngredientDic { get; private set; }
    [SerializeField] private List<ItemDataIngredientSO> _ingredientList = new();

    public ItemDataBreadSO SelectingCakeRecipeData { get; set; }

    private void Awake()
    {
        usedIngredientStash = new UsedIngredientStash(transform);

        BreadDictionary = new Dictionary<string, ItemDataBreadSO>();
        IngredientDic = new Dictionary<string, ItemDataIngredientSO>();

        foreach(ItemDataBreadSO cake in _breadList)
        {
            BreadDictionary.Add(cake.itemName, cake);
        }

        foreach(ItemDataIngredientSO ing in _ingredientList)
        {
            IngredientDic.Add(ing.itemName, ing);
        }
    }
    public void AddItem(ItemDataSO item)
    {
        if (usedIngredientStash.CanAddItem(item))
        {
            usedIngredientStash.AddItem(item);
        }
    }
    public void RemoveItem(ItemDataSO item)
    {
        ItemDataIngredientSO ingredientSO = ((ItemDataIngredientSO)item);
        if (ingredientSO != null)
        {
        }
        usedIngredientStash.RemoveItem(item);
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

        string[] names = new string[3];
        for (int i = 0; i < 3; ++i)
        {
            names[i] = usedIngredientStash.usedIngredientStash[i].itemDataSO.itemName;
        }
        ItemDataBreadSO returnBread = _recipeTable.Bake(names);
        if (returnBread != null)
        {
            Inventory.Instance.AddItem(returnBread);
            usedIngredientStash.RemoveAllItem();
        }

        return returnBread;
    }
    public ItemDataBreadSO GetCakeDataByName(string cakeName)
    {
        if(!BreadDictionary.ContainsKey(cakeName))
        {
            Debug.LogError($"{cakeName} is Not Exist!");
            return null;
        }

        return BreadDictionary[cakeName];
    }
    public ItemDataIngredientSO GetIngredientDataByName(string ingredientName)
    {
        if(!IngredientDic.ContainsKey(ingredientName))
        {
            Debug.LogError($"{ingredientName} is Not Exist!");
            return null;
        }

        return IngredientDic[ingredientName];
    }
    public ItemDataIngredientSO[] GetIngredientDatasByCakeName(string cakeName)
    {
        string[] ingNames = _recipeTable.GetIngredientNamesByCakeName(cakeName);

        ItemDataIngredientSO[] datas = new ItemDataIngredientSO[3]
        {
            GetIngredientDataByName(ingNames[0]),
            GetIngredientDataByName(ingNames[1]),
            GetIngredientDataByName(ingNames[2])
        };

        return datas;
    }

    [ContextMenu("GET_ALL_INGREDIENT")]
    public void TEST_Get_All_Ingredient_Item()
    {
        foreach(var item in _ingredientList)
        {
            Inventory.Instance.AddItem(item);
        }
    }
}