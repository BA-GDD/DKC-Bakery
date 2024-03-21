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
    [Header("페이지에 따른 몬스터 출현 기믹")]
    [SerializeField] private int _divineToken;
    private int _divineCount;
    [SerializeField] private SEList<SEList<Enemy>> _monsterGimicList = new();

    //[HideInInspector]
    public Enemy[] onFieldMonsterList;
    private EnemyHpBarMaker _enemyHpBarMaker;

    [Header("출현 텀")]
    [SerializeField] [Range(0.01f, 0.1f)] private float _spawnTurm;

    [SerializeField] private EnemyGroupSO _enemyGroup;

    [SerializeField] private List<Transform> _spawnDistanceByPoint = new();
    private Queue<PoolingType> _enemyQue = new Queue<PoolingType>();
    private Queue<int> _enemyDeadQue = new Queue<int>();

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
        _enemyHpBarMaker = FindObjectOfType<EnemyHpBarMaker>();

        TurnCounter.RoundEndEvent += HandleRoundEnd;
        TurnCounter.EnemyTurnStartEvent += HandleEnemyTurnStart;
        TurnCounter.EnemyTurnEndEvent += HandleEnemyTurnEnd;

        onFieldMonsterList = new Enemy[_spawnDistanceByPoint.Count];

    }
    private void OnDestroy()
    {
        TurnCounter.RoundEndEvent -= HandleRoundEnd;
        TurnCounter.EnemyTurnStartEvent -= HandleEnemyTurnStart;
        TurnCounter.EnemyTurnEndEvent -= HandleEnemyTurnEnd;
    }
    private void HandleRoundEnd()
    {

    }
    private void HandleEnemyTurnStart()
    {
        Debug.Log("Enemy Turn Start");
        foreach (var e in onFieldMonsterList)
        {
            e.TurnStart();
        }
        if (onFieldMonsterList.Length > 0)
            EnemySquence();
    }
    private void HandleEnemyTurnEnd()
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

            onFieldMonsterList[idx] = selectEnemy;
            selectEnemy.Spawn(pos);
        }
    }

    private void HandleChangeMonsterCountOnField(object sender, EventArgs e)
    {
        //if (onFieldMonsterList.Count == 0)
        //{
        //    _divineCount++;

        //    if ((_divineCount % _divineToken) == 0)
        //    {
        //        Debug.Log("Phase Clear");
        //        _currentStage.CurPhaseCleared = true;
        //        StartCoroutine(SpawnMonster());
        //    }

        //    StartCoroutine(SpawnMonster());
        //}
        StartCoroutine(SpawnInitMonster());
    }
}
