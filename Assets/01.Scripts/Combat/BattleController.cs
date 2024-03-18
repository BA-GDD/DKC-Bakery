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
    [Header("�������� ���� ���� ���� ���")]
    [SerializeField] private int _divineToken;
    private int _divineCount;
    [SerializeField] private SEList<SEList<Enemy>> _monsterGimicList = new();

    [HideInInspector]
    public ExpansionList<Enemy> onFieldMonsterList = new ExpansionList<Enemy>();
    private EnemyHpBarMaker _enemyHpBarMaker;

    [Header("���� ��")]
    [SerializeField] [Range(0.01f, 0.1f)] private float _spawnTurm;

    [SerializeField]private EnemyGroupSO _enemyGroup;

    [SerializeField] private List<Transform> _spawnDistanceByPoint = new();
    private Queue<PoolingType> _enemyQue = new Queue<PoolingType>();

    private void Awake()
    {
        _enemyHpBarMaker = FindObjectOfType<EnemyHpBarMaker>();

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
            yield return new WaitUntil(() => e.isTurnEnd);
        }
        TurnCounter.ChangeTurn();
    }

    public void SetStage()
    {
        foreach (var e in _enemyGroup.enemies)
        {
            _enemyQue.Enqueue(e.poolingType);
        }

        _enemyGroup = MapManager.Instanace.SelectStageData.enemyGroup;
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
        //while (_enemyQue.Count > 0)
        //{
        //    if (onFieldMonsterList.Count >= _spawnDistanceByPoint.Count)
        //        break;
        //    Vector3 pos = _spawnDistanceByPoint[onFieldMonsterList.Count].position;
        //    Enemy selectEnemy = PoolManager.Instance.Pop(_enemyQue.Dequeue()) as Enemy;
        //    selectEnemy.BattleController = this;
        //    selectEnemy.transform.position = pos;

        //    onFieldMonsterList.Add(selectEnemy);
        //}
        _enemyHpBarMaker.SetupEnemyHpBar();
        //if(_monsterGimicList.list.Count >= _divineCount)
        //{
        //    Debug.Log($"{_monsterGimicList.list.Count}, {_divineCount}");
        //    for (int i = 0; i < _monsterGimicList.list[_divineCount].list.Count; i++)
        //    {
        //        yield return new WaitForSeconds(_spawnTurm);

        //        Enemy selectEnemy = _monsterGimicList.list[_divineCount].list[i];
        //        selectEnemy.gameObject.SetActive(true);

        //        Color startColor = new Color(1, 1, 1, 0);
        //        selectEnemy.SpriteRendererCompo.color = startColor;
        //        selectEnemy.SpriteRendererCompo.DOFade(1, 0.1f);

        //        onFieldMonsterList.Add(selectEnemy);
        //    }

        //    _enemyHpBarMaker.SetupEnemyHpBar();
        //}
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

            selectEnemy.HealthCompo.OnDeathEvent.AddListener(() => DeadMonster(selectEnemy));

            if (idx >= onFieldMonsterList.Count)
                onFieldMonsterList.Add(selectEnemy);
            else
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

    public void DeadMonster(Enemy enemy)
    {
        int idx = onFieldMonsterList.IndexOf(enemy);
        SpawnMonster(idx);
    }
}
