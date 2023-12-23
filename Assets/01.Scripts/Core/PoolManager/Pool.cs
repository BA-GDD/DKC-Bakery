using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Queue<T> _pool = new Queue<T>();
    private Transform _parnet;
    private T _prefab;

    public Pool(T prefab, Transform parent, int count = 20)
    {
        _prefab = prefab;
        _parnet = parent;
        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(_prefab, _parnet);
            obj.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
    public T Pop()
    {
        T obj = null;
        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
        }
        else
        {
            obj = GameObject.Instantiate(_prefab, _parnet);
            obj.name.Replace("(Clone)", "");
        }
        obj.gameObject.SetActive(true);
        obj.Init();
        return obj;
    }

}
