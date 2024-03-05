using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemDataSO itemDataSO;
    public int stackSize;

    public InventoryItem(ItemDataSO itemDataSO)
    {
        this.itemDataSO = itemDataSO;
        AddStack(1);
    }

    public void AddStack(int count = 1)
    {
        stackSize += count;
    }

    public void RemoveStack(int count)
    {
        stackSize -= count;
    }
}
