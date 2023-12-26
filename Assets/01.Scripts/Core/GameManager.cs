using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Player _player;
    public Transform PlayerTrm => _player.transform;
    public Player Player => _player;

    [Header("Pooling")]
    [SerializeField] private PoolListSO _poolingList;
    [SerializeField] private Transform _poolingTrm;
    private void Awake()
    {
        PoolManager.Instance = new PoolManager(_poolingTrm);
        foreach (PoolingItem item in _poolingList.poolList)
        {
            PoolManager.Instance.CreatePool(item.prefab, item.type, item.count);
        }
    }
}