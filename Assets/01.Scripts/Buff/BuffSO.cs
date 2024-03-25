using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NormalBuff
{
    public StatType type;
    public int value;
}

[CreateAssetMenu(menuName = "SO/Buff")]
public class BuffSO : ScriptableObject
{
    private Entity _owner;
    private CharacterStat _stat;

    public List<NormalBuff> statBuffs = new();
    public List<SpecialBuff> specialBuffs = new();

    public void Clone()
    {
        for (int i = 0; i < specialBuffs.Count; i++)
        {
            specialBuffs[i] = Instantiate(specialBuffs[i]);
        }
    }

    public void SetOwner(Entity owner)
    {
        _owner = owner;
        _stat = owner.CharStat;
        specialBuffs.ForEach(b => b.SetOwner(owner));
    }

    public void AppendBuff()
    {
        foreach (var b in statBuffs)
        {
            _stat.IncreaseStatBy(b.value, _stat.GetStatByType(b.type));
        }
        foreach (var b in specialBuffs)
        {
            _owner.BuffStatCompo.specialBuffList.Add(b);
            if (b is IOnTakeDamage i)
            {
                if (!_owner.OnAttack.Contains(i))
                    _owner.OnAttack.Add(i);
            }
        }
    }

    public void UpdateBuff()
    {
        foreach (var b in specialBuffs)
        {
            b.Active();
        }
    }

    public void PrependBuff()
    {
        foreach (var b in statBuffs)
        {
            _stat.DecreaseStatBy(b.value, _stat.GetStatByType(b.type));
        }
        foreach (var b in specialBuffs)
        {
            //    if (b is IOnTakeDamage i)
            //        _owner.OnAttack += i.TakeDamage;
        }
    }
}
