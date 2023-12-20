using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/Item")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;

    [Range(0, 100)]
    public float dropChance = 50f;

    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}
