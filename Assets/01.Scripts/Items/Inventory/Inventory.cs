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
    [SerializeField] private ItemDataSO _testBaseData;
    [SerializeField] private ItemDataSO _testLiquidData;
    [SerializeField] private ItemDataSO _testLeavenData;
    [SerializeField] private ItemDataSO _testButterfatData;
    [SerializeField] private ItemDataSO _testSugarsData;

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
            Debug.Log("gKey");
            AddItem(_debugItemData);
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            Debug.Log("yKey");
            AddItem(_testBaseData);
        }
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            Debug.Log("uKey");
            AddItem(_testLiquidData);
        }
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            Debug.Log("iKey");
            AddItem(_testLeavenData);
        }
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            Debug.Log("oKey");
            AddItem(_testButterfatData);
        }
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            AddItem(_testSugarsData);
        }
    }
    public void UpdateSlotUI() 
    {
        ingredientStash.UpdateSlotUI();
        breadStash.UpdateSlotUI();
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
            Debug.Log("itemType is Ingredient");
            if (ingredientStash.CanAddItem(item))
            {
                Debug.Log("Can Add Item");
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
