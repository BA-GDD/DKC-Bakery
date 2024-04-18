using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnHitDamage<T1, T2>(T1 t1, ref T2 t2);
public class BuffStat
{
    public AilmentEnum currentAilment;

    public OnHitDamage<Entity, int> OnHitDamageEvent;

    public List<SpecialBuff> specialBuffList = new();
    private Entity _owner;
    private Dictionary<BuffSO, int> _buffDic = new();

    public BuffStat(Entity entity)
    {
        _owner = entity;
        _buffDic = new();
        TurnCounter.RoundStartEvent += UpdateBuff;
        //_owner.BeforeChainingEvent.AddListener(UpdateBuff);
    }
    public void AddBuff(BuffSO so, int durationTurn, int combineLevel = 0)
    {
        so.SetOwner(_owner);
        if (_buffDic.ContainsKey(so))
        {
            if (_buffDic[so] < durationTurn)
                _buffDic[so] = durationTurn;
        }
        else
        {
            so.AppendBuff(combineLevel);
            _buffDic.Add(so, durationTurn);
        }
    }
    public void ActivateSpecialBuff(SpecialBuff buff)
    {
        specialBuffList.Add(buff);
        if (buff is IOnTakeDamage)
        {
            IOnTakeDamage i = buff as IOnTakeDamage;
            if (!_owner.OnAttack.Contains(i))
                _owner.OnAttack.Add(i);
        }

        if (buff is IOnRoundStart)
        {
            IOnRoundStart i = buff as IOnRoundStart;
            TurnCounter.RoundStartEvent += i.RoundStart;
        }

        if (buff is IOnHItDamage)
        {
            IOnHItDamage i = buff as IOnHItDamage;
            OnHitDamageEvent += i.HitDamage;
        }
    }
    public void CompleteBuff(SpecialBuff special)
    {
        if (special is IOnTakeDamage)
        {
            IOnTakeDamage i = special as IOnTakeDamage;
            if (_owner.OnAttack.Contains(i))
                _owner.OnAttack.Remove(i);
        }
        if (special is IOnRoundStart)
        {
            IOnRoundStart i = special as IOnRoundStart;
            TurnCounter.RoundStartEvent -= i.RoundStart;
        }
        if (special is IOnHItDamage)
        {
            IOnHItDamage i = special as IOnHItDamage;
            OnHitDamageEvent -= i.HitDamage;
            //_owner.HealthCompo.OnHitEvent.RemoveListener(i.HitDamage);
        }
        specialBuffList.Remove(special);
    }
    public void UpdateBuff()
    {
        foreach (var d in _buffDic.Keys.ToList())
        {
            d.Update();

            _buffDic[d]--;
            if (_buffDic[d] <= 0)
            {
                d.PrependBuff();
                _buffDic.Remove(d);
                continue;
            }
        }
    }
    public void ClearStat()
    {
        foreach (var d in _buffDic.Keys.ToList())
        {
            d.PrependBuff();
            _buffDic.Remove(d);
        }
        while(specialBuffList.Count > 0)
        {
            CompleteBuff(specialBuffList[0]);
        }
        TurnCounter.RoundStartEvent -= UpdateBuff;
    }
}
