using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialStash : Stash
{
    public MaterialStash(Transform slotParentTrm) : base(slotParentTrm)
    {
    }

    public override void AddItem(ItemDataSO item, int count = 1)
    {
        if (stashDictionary.TryGetValue(item, out InventoryItem invenItem))
        {
            invenItem.AddStack(count);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            newItem.AddStack(count - 1);
            stash.Add(newItem);
            stashDictionary.Add(item, newItem);
        }
    }

    public override bool CanAddItem(ItemDataSO item)
    {
        if (stash.Count >= _itemSlots.Length && !stashDictionary.ContainsKey(item))
        {
            Debug.Log("����á��");
            return false;
        }
        return true;
    }

    public override void RemoveItem(ItemDataSO item, int count = 1)
    {
        if (stashDictionary.TryGetValue(item, out InventoryItem invenItem))
        {
            if (invenItem.stackSize <= count)
            {
                stash.Remove(invenItem);
                stashDictionary.Remove(item);
            }
            else
            {
                invenItem.RemoveStack(count);
            }
        }
    }
}
