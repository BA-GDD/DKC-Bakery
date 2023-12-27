using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedIngredientStash : Stash
{
    public Dictionary<IngredientType, InventoryItem> usedIngredDictionary;

    public UsedIngredientStash(Transform slotParentTrm) : base(slotParentTrm)
    {
    }

    public override void AddItem(ItemDataSO item, int count = 1)
    {
        // 그냥 Dictionary key만 enum으로 바꾼다.
        // CanAddItem에서 같은 분류의 재료가 이미 들어가 있는지 확인헀기 때문에 그냥 넣음
        InventoryItem newItem = new InventoryItem(item);
        stash.Add(newItem);
        usedIngredDictionary.Add(((ItemDataIngredientSO)item).ingredientType, newItem);
    }

    public override bool CanAddItem(ItemDataSO item)
    {
        // TryGetValue하는데 딕셔너리에서 내가 원하는 분류를 가져와야함
        // 지금은 
        if (usedIngredDictionary.TryGetValue(((ItemDataIngredientSO)item).ingredientType, out InventoryItem invenItem))
        {
            // 만약 추가하려는 재료와 같은 분류의 재료가 들어가 있을 경우 넣을 수 없음
            if (((ItemDataIngredientSO)invenItem.itemDataSO).ingredientType == ((ItemDataIngredientSO)item).ingredientType)
            {
                return false;
            }
        }
        return true;
    }

    public override void RemoveItem(ItemDataSO item, int count = 1)
    {
        if(stashDictionary.TryGetValue(item, out InventoryItem invenItem))
        {
            stash.Remove(invenItem);
            stashDictionary.Remove(item);
        }
    }
}
