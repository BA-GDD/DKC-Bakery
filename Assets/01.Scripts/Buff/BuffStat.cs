using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStat
{
    private Entity _owner;
    private Dictionary<BuffSO, int> _buffDic = new();

    public List<SpecialBuff> specialBuffList = new();

    public BuffStat(Entity entity)
    {
        _owner = entity;
        _buffDic = new();
        TurnCounter.RoundStartEvent += UpdateBuff;
        _owner.BeforeChainingEvent.AddListener(UpdateBuff);
    }

    public void AddBuff(BuffSO so, int durationTurn)
    {
        so.SetOwner(_owner);
        if (_buffDic.ContainsKey(so))
        {
            if (_buffDic[so] < durationTurn)
                _buffDic[so] = durationTurn;
        }
        else
        {
            so.AppendBuff();
            _buffDic.Add(so, durationTurn);
        }
    }
    public void EndCardCheckDel()
    {
        foreach (var special in specialBuffList)
        {
            if (special.GetIsComplete())
            {
                if (special is IOnTakeDamage i)
                {
                    if (_owner.OnAttack.Contains(i))
                        _owner.OnAttack.Remove(i);
                }
            }
        }
    }
    public void UpdateBuff()
    {
        foreach (var d in _buffDic)
        {
            d.Key.UpdateBuff();
            _buffDic[d.Key]--;
            if (_buffDic[d.Key] <= 0)
            {
                d.Key.PrependBuff();
                _buffDic.Remove(d.Key);
                continue;
            }

        }
    }
}
