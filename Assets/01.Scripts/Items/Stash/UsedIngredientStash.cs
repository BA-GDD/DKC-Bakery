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
        // �׳� Dictionary key�� enum���� �ٲ۴�.
        // CanAddItem���� ���� �з��� ��ᰡ �̹� �� �ִ��� Ȯ������ ������ �׳� ����
        InventoryItem newItem = new InventoryItem(item);
        stash.Add(newItem);
        usedIngredDictionary.Add(((ItemDataIngredientSO)item).ingredientType, newItem);
    }

    public override bool CanAddItem(ItemDataSO item)
    {
        // TryGetValue�ϴµ� ��ųʸ����� ���� ���ϴ� �з��� �����;���
        // ������ 
        if (usedIngredDictionary.TryGetValue(((ItemDataIngredientSO)item).ingredientType, out InventoryItem invenItem))
        {
            // ���� �߰��Ϸ��� ���� ���� �з��� ��ᰡ �� ���� ��� ���� �� ����
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
