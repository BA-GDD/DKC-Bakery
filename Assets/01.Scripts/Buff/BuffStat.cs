using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStat
{
    public AilmentEnum currentAilment;

    private Entity _owner;
    private Dictionary<BuffSO, int> _buffDic;

    public BuffStat(Entity entity)
    {
        _owner = entity;
    }
    public void AddBuff(BuffSO so, int durationTurn)
    {
        so.SetOwner(_owner);
        if (_buffDic.ContainsKey(so))
        {
            if(_buffDic[so] < durationTurn)
                _buffDic[so] = durationTurn;
        }
        else
        {
            so.AppendBuff();
            _buffDic.Add(so, durationTurn);
        }
    }

    public void UpdateBuff()
    {
        foreach (var d in _buffDic)
        {
            d.Key.Update();

            _buffDic[d.Key]--;
            if(_buffDic[d.Key] <= 0)
            {
                d.Key.PrependBuff();
                _buffDic.Remove(d.Key);
                continue;
            }

        }
    }
}