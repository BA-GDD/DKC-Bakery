using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaTimeUI : SceneUI
{
    [SerializeField] private ItemDataBreadSO _sampleCake;

    private void Start()
    {
        InventoryItem ii = new InventoryItem(_sampleCake);
        Inventory.Instance.breadStash.stash.Add(ii);
    }
}
