using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedIngredientStash : Stash
{
    public Dictionary<IngredientType, InventoryItem> usedIngredDictionary;
    public List<InventoryItem> usedIngredientStash;

    public UsedIngredientStash(Transform slotParentTrm) : base(slotParentTrm)
    {
        usedIngredientStash = new List<InventoryItem>();
        for (int i = 0; i < 5; ++i)
        {
            usedIngredientStash.Add(null);
        }
        usedIngredDictionary = new Dictionary<IngredientType, InventoryItem>();

        _slotParentTrm = slotParentTrm;
        _itemSlots = _slotParentTrm.GetComponentsInChildren<ItemSlot>();
    }

    public override void AddItem(ItemDataSO item, int count = 1)
    {
        // �׳� Dictionary key�� enum���� �ٲ۴�.
        // CanAddItem���� ���� �з��� ��ᰡ �̹� ��  �ִ��� Ȯ������ ������ �׳� ����
        InventoryItem newItem = new InventoryItem(item);
        //stash.Add(newItem);
        //usedIngredientStash.Add(newItem);
        usedIngredientStash[(int)((ItemDataIngredientSO)item).ingredientType - 1] = newItem;
        //Debug.Log((int)((ItemDataIngredientSO)item).ingredientType - 1);

        usedIngredDictionary.Add(((ItemDataIngredientSO)item).ingredientType, newItem);
    }

    public override void UpdateSlotUI()
    {
        //base.UpdateSlotUI();
        for (int i = 0; i < _itemSlots.Length; ++i)
        {
            _itemSlots[i].CleanUpSlot();
        }

        for (int i = 0; i < usedIngredientStash.Count; ++i)
        {
            _itemSlots[i].UpdateSlot(usedIngredientStash[i]);
        }
    }

    public override bool CanAddItem(ItemDataSO item)
    {
        // ���� �߰��Ϸ��� ���� ���� �з��� ��ᰡ �� ���� ��� ���� �� ����
        if (usedIngredDictionary.TryGetValue(((ItemDataIngredientSO)item).ingredientType, out InventoryItem invenItem))
        {
            return false;
        }
        return true;
    }

    public override void RemoveItem(ItemDataSO item, int count = 1)
    {
        // ���� �ش� �з��� ��ᰡ ���������
        if (usedIngredDictionary.TryGetValue(((ItemDataIngredientSO)item).ingredientType, out InventoryItem invenItem))
        {
            // stash���� �����
            //stash.Remove(invenItem);
            //usedIngredientStash.Remove(invenItem);
            usedIngredientStash[(int)((ItemDataIngredientSO)item).ingredientType - 1] = null;
            // Dictionary���� ����
            usedIngredDictionary.Remove(((ItemDataIngredientSO)item).ingredientType);
        }

        Inventory.Instance.AddItem(item, count);
    }

    public void RemoveAllItem()
    {
        for(int i = 0; i < 5; ++i)
        {
            usedIngredDictionary.Remove(((ItemDataIngredientSO)usedIngredientStash[i].itemDataSO).ingredientType);
            usedIngredientStash[i] = null;
        }
    }
}
