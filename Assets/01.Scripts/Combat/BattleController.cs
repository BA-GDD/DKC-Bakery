using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Threading.Tasks;

[Serializable]
public class SEList<T>
{
    public List<T> list;
}

public class BattleController : MonoBehaviour
{
    [SerializeField] private SEList<SEList<bool>> isStuck;

    public Enemy[] onFieldMonsterList;
    private EnemyHpBarMaker _enemyHpBarMaker;

    [Header("√‚«ˆ ≈“")]
    [SerializeField] [Range(0.01f, 0.1f)] private float _spawnTurm;

    [SerializeField] private EnemyGroupSO _enemyGroup;

    [SerializeField] private List<Transform> _spawnDistanceByPoint = new();
    private Queue<PoolingType> _enemyQue = new Queue<PoolingType>();

    [SerializeField] private Player _player;
    public Player Player
    {
        get
        {
            if (_player != null) return _player;
            _player = FindObjectOfType<Player>();
            return _player;
        }
    }

    private void Awake()
    {
        //_enemyHpBarMaker = FindObjectOfType<EnemyHpBarMaker>();

        TurnCounter.RoundEndEvent += HandleRoundEnd;
        TurnCounter.EnemyTurnStartEvent += HandleEnemyTurnStart;
        TurnCounter.EnemyTurnEndEvent += HandleEnemyTurnEnd;
    }
    private void OnDestroy()
    {
        TurnCounter.RoundEndEvent -= HandleRoundEnd;
        TurnCounter.EnemyTurnStartEvent -= HandleEnemyTurnStart;
        TurnCounter.EnemyTurnEndEvent -= HandleEnemyTurnEnd;
    }

    private void HandleRoundEnd()
    {
        StartCoroutine(RechargeEnemy());
    }

    private void HandleEnemyTurnStart()
    {
        foreach (var e in onFieldMonsterList)
        {
            if (e is null) continue;
            e.TurnStart();
        }
        if (onFieldMonsterList.Length > 0)
            StartCoroutine(EnemySquence());
    }
    private void HandleEnemyTurnEnd()
    {
        foreach (var e in onFieldMonsterList)
        {
            if (e is null) continue;
            e.TurnEnd();
        }
    }

    private IEnumerator EnemySquence()
    {
        Debug.Log(onFieldMonsterList.Length);
        foreach (var e in onFieldMonsterList)
        {
            if (e is null) continue;
            e.TurnAction();
            yield return new WaitUntil(() => e.turnStatus == TurnStatus.End);
        }
        TurnCounter.ChangeTurn();
    }

    private void Start()
    {
        SetStage(MapManager.Instanace.SelectStageData.enemyGroup);
    }

    public void SetStage(EnemyGroupSO groupSO)
    {
        _enemyGroup = groupSO;

        foreach (var e in _enemyGroup.enemies)
        {
            _enemyQue.Enqueue(e.poolingType);
        }
        onFieldMonsterList = new Enemy[_spawnDistanceByPoint.Count];
        StartCoroutine(RechargeEnemy());
    }

    private IEnumerator RechargeEnemy()
    {
        for (int i = 0; i < _spawnDistanceByPoint.Count; i++)
        {
            if (onFieldMonsterList[i] != null) continue;

            SpawnMonster(i);
            yield return new WaitForSeconds(_spawnTurm);
        }

        //_enemyHpBarMaker.SetupEnemyHpBar();
    }
    private void SpawnMonster(int idx)
    {
        if (_enemyQue.Count > 0)
        {
            Vector3 pos = _spawnDistanceByPoint[idx].position;
            Enemy selectEnemy = PoolManager.Instance.Pop(_enemyQue.Dequeue()) as Enemy;
            selectEnemy.BattleController = this;
            selectEnemy.transform.position = pos;
            selectEnemy.BattleController = this;
            selectEnemy.SpriteRendererCompo.sortingOrder = (idx + 2) % 2;

            selectEnemy.HealthCompo.OnDeathEvent.AddListener(() => DeadMonster(selectEnemy));

            onFieldMonsterList[idx] = selectEnemy;
            selectEnemy.Spawn(pos);
        }
    }
    private void DeadMonster(Enemy enemy)
    {
        onFieldMonsterList[Array.IndexOf(onFieldMonsterList, enemy)] = null;
    }
    public bool IsStuck(int to, int who)
    {
        return isStuck.list[to].list[who];
    }
}
