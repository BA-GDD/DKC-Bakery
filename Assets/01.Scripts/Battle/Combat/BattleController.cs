using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[Serializable]
public class SEList<T>
{
    public List<T> list;
}

public class BattleController : MonoBehaviour
{
    [SerializeField] private SEList<SEList<bool>> isStuck;

    public Enemy[] onFieldMonsterList;
    public List<Enemy> DeathEnemyList { get; set; } = new List<Enemy>();


    //[HideInInspector]
    private EnemyHpBarMaker _enemyHpBarMaker;

    [Header("���� ��")]
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
        onFieldMonsterList = new Enemy[_spawnDistanceByPoint.Count];

        TurnCounter.EnemyTurnStartEvent += OnEnemyTurnStart;
        TurnCounter.EnemyTurnEndEvent += OnEnemyTurnEnd;
    }
    private void OnDestroy()
    {
        TurnCounter.EnemyTurnStartEvent -= OnEnemyTurnStart;
        TurnCounter.EnemyTurnEndEvent -= OnEnemyTurnEnd;
    }

    private void OnEnemyTurnStart()
    {
        foreach (var e in onFieldMonsterList)
        {
            e?.TurnStart();
        }
        StartCoroutine(EnemySquence());
    }
    private void OnEnemyTurnEnd()
    {
        foreach (var e in onFieldMonsterList)
        {
            e?.TurnEnd();
        }
    }

    private IEnumerator EnemySquence()
    {
        foreach (var e in onFieldMonsterList)
        {
            if (e is null) continue;

            e.TurnAction();
            yield return new WaitUntil(() => e.turnStatus == TurnStatus.End);
            yield return new WaitForSeconds(1.5f);
        }
        TurnCounter.ChangeTurn();
    }

    public void SetStage()
    {
        _enemyGroup = MapManager.Instanace.SelectStageData.enemyGroup;

        foreach (var e in _enemyGroup.enemies)
        {
            _enemyQue.Enqueue(e.poolingType);
        }
        StartCoroutine(SpawnInitMonster());
    }

    private IEnumerator SpawnInitMonster()
    {
        yield return null;

        for (int i = 0; i < _spawnDistanceByPoint.Count; i++)
        {
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
            PoolingType y = _enemyQue.Dequeue();
            Enemy selectEnemy = PoolManager.Instance.Pop(y) as Enemy;
            print(y);
            selectEnemy.BattleController = this;
            selectEnemy.transform.position = pos;
            selectEnemy.BattleController = this;
            int posChecker = ((idx + 3) % 2) * 2;
            selectEnemy.SpriteRendererCompo.sortingOrder = posChecker;

            selectEnemy.HealthCompo.OnDeathEvent.AddListener(() => DeadMonster(selectEnemy));

            onFieldMonsterList[idx] = selectEnemy;
            selectEnemy.Spawn(pos);
        }
    }

    public void DeadMonster(Enemy enemy)
    {
        onFieldMonsterList[Array.IndexOf(onFieldMonsterList, enemy)] = null;
        DeathEnemyList.Add(enemy);
    }
    public bool IsStuck(int to, int who)
    {
        return isStuck.list[to].list[who];
    }
}
