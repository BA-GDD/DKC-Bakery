using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Flags]
public enum IngredientType
{
    None = 0,       // 绝澜 (眉农侩)
    Core = 1,       // 海捞胶
    Trace = 2,     // 如利
    Subjectivity = 4,     // 林包
    Else = 8
}

public class BakingManager : MonoSingleton<BakingManager>
{
    public UsedIngredientStash usedIngredientStash;

    [SerializeField]
    private BreadRecipeTable _recipeTable;

    public Dictionary<string, ItemDataBreadSO> BreadDictionary { get; private set; }
    [SerializeField]
    private List<ItemDataBreadSO> _breadList;

    private void Awake()
    {
        usedIngredientStash = new UsedIngredientStash(transform);
        BreadDictionary = new Dictionary<string, ItemDataBreadSO>();

        for (int i = 0; i < _breadList.Count; ++i)
        {
            BreadDictionary.Add(_breadList[i].itemName, _breadList[i]);
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
}