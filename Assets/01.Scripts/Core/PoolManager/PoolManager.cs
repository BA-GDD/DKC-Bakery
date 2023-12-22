using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<string, Pool<PoolableMono>> _poolDic = new Dictionary<string, Pool<PoolableMono>>();

    public void Init(PoolListSO list)
    {
        for (int i = 0; i < list.poolList.Count; i++)
        {
            PoolingItem item = list.poolList[i];
            _poolDic.Add(item.prefab.name, new Pool<PoolableMono>(item.prefab, transform, item.count));
        }
    }
    public void Push(PoolableMono obj)
    {
        if (_poolDic.ContainsKey(obj.name))
            _poolDic[obj.name].Push(obj);
        else
            Debug.LogError($"not have ${obj.name} pool");
    }
    public PoolableMono Pop(string name)
    {
        PoolableMono obj = null;
        if (!_poolDic.ContainsKey(name))
        {
            Debug.LogError($"not have ${name} pool");
        }
        obj = _poolDic[name].Pop();
        return obj;
    }
}
