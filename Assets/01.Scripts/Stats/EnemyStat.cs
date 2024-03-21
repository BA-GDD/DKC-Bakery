using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Enemy")]
public class EnemyStat : CharacterStat
{
<<<<<<< HEAD
    [Header("µå·Ó¾ÆÀÌÅÛ")]
    [SerializeField] private ItemDataIngredientSO _dropIngredientItem;
    public ItemDataIngredientSO DropItem => _dropIngredientItem;

=======
>>>>>>> parent of 8b20a26 (0321 ë¨¸ì§€ ì „ ì»¤ë°‹)
    [Header("Level detail")]
    [SerializeField] private int _level;

    [Range(0, 1f)]
    [SerializeField] private float _percentageModifier;

    public void Modify(Stat stat)
    {
        for (int i = 1; i < _level; i++)
        {
            //·¹º§´ç Áõ°¡ÇÏ°Ô µÊ.
            float modifier = stat.GetValue() * _percentageModifier;
            stat.AddModifier(Mathf.RoundToInt(modifier));
        }
    }
}