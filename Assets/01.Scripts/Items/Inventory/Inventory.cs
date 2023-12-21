using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Inventory : MonoSingleton<Inventory>
{
    public IngredientStash ingredientStash;
    public BreadStash breadStash;

    [Header("ParentTrms")]
    [SerializeField] private Transform _ingredientParent; 
    [SerializeField] private Transform _breadParent;
    

    [Header("Events")]
    public UnityEvent<int> onRemoveBreadTrigger; 
    public UnityEvent<int> onRemoveIngredientTrigger; 

    [Header("Debug")]
    [SerializeField] private ItemDataSO _debugItemData;
    [SerializeField] private string _debugText;

    private void Awake()
    {
        ingredientStash = new IngredientStash(_ingredientParent);
        breadStash = new BreadStash(_breadParent);

    }
    private void Start()
    {
        UpdateSlotUI();
    }
    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            AddItem(_debugItemData);
        }
    }
    public void UpdateSlotUI() 
    {
        ingredientStash.UpdateSlotUI();
        breadStash.UpdateSlotUI();
    }
    public void AddItem(ItemDataSO item, int count = 1)
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
            if (ingredientStash.CanAddItem(item))
            {
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

    public void DebugText(int index)
    {
        Debug.Log($"{ _debugText} : { index}");
    }
}
