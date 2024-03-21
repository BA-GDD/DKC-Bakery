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
    public List<Enemy> DeathEnemyList { get; set; }  = new List<Enemy>();


    //[HideInInspector]
    private EnemyHpBarMaker _enemyHpBarMaker;

    [Header("���� ��")]
    [SerializeField] [Range(0.01f, 0.1f)] private float _spawnTurm;

    [SerializeField]private EnemyGroupSO _enemyGroup;

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
        Debug.Log("Enemy Turn Start");
        foreach (var e in onFieldMonsterList)
        {
            e.TurnStart();
        }
        if (onFieldMonsterList.Count > 0) StartCoroutine(EnemySquence());
    }
    private void OnEnemyTurnEnd()
    {
        foreach (var e in onFieldMonsterList)
        {
            e.TurnEnd();
        }
    }

    private IEnumerator EnemySquence()
    {
        foreach (var e in onFieldMonsterList)
        {
            e.TurnAction();
            Debug.Log($"��ΰ���?{onFieldMonsterList.Count}");
            yield return new WaitUntil(() => e.turnStatus == TurnStatus.End);
        }
        TurnCounter.TurnCounting.ToPlayerTurnChanging(true);
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
            Enemy selectEnemy = PoolManager.Instance.Pop(_enemyQue.Dequeue()) as Enemy;
            selectEnemy.BattleController = this;
            selectEnemy.transform.position = pos;
            selectEnemy.BattleController = this;
            selectEnemy.SpriteRendererCompo.sortingOrder = (idx + 2) % 2;

            selectEnemy.HealthCompo.OnDeathEvent.AddListener(() => DeadMonster(selectEnemy));

            if (idx >= onFieldMonsterList.Count)
                onFieldMonsterList.Add(selectEnemy);
            else
                onFieldMonsterList[idx] = selectEnemy;
            selectEnemy.Spawn(pos);
        }
    }

    public void DeadMonster(Enemy enemy)
    {
        onFieldMonsterList[Array.IndexOf(onFieldMonsterList, enemy)] = null;
    }
    public bool IsStuck(int to, int who)
    {
        return isStuck.list[to].list[who];
    }
}
