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

    [HideInInspector]
    public ExpansionList<Enemy> onFieldMonsterList = new ExpansionList<Enemy>();
    private Stage _currentStage;
    private EnemyHpBarMaker _enemyHpBarMaker;

    [Header("출현 텀")]
    [SerializeField] [Range(0.01f, 0.1f)] private float _spawnTurm;



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
        if(onFieldMonsterList.Count > 0) StartCoroutine(EnemySquence());
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

    private void Start()
    {
        //for (int i = 0; i < _monsterGimicList.list.Count; i++)
        //{
        //    for (int j = 0; j < _monsterGimicList.list[i].list.Count; j++)
        //    {
        //        _monsterGimicList.list[i].list[j].gameObject.SetActive(false);
        //    }
        //}

        _currentStage = FindObjectOfType<Stage>();
        onFieldMonsterList.ListChanged += HandleChangeMonsterCountOnField;

        //_currentStage.OnPhaseCleared += () => StartCoroutine(SpawnMonster());
        foreach (var e in _currentStage.enemyGroup.enemies)
        {
            _enemyQue.Enqueue(e.poolingType);
        }
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        yield return null;
        while (_enemyQue.Count > 0)
        {
            if (onFieldMonsterList.Count >= _spawnDistanceByPoint.Count)
                break;
            Vector3 pos = _spawnDistanceByPoint[onFieldMonsterList.Count].position;
            Enemy selectEnemy = PoolManager.Instance.Pop(_enemyQue.Dequeue()) as Enemy;
            selectEnemy.BattleController = this;
            selectEnemy.transform.position = pos;

            onFieldMonsterList.Add(selectEnemy);
        }
        _enemyHpBarMaker.SetupEnemyHpBar();
        //if(_monsterGimicList.list.Count >= _divineCount)
        //{
        //    Debug.Log($"{_monsterGimicList.list.Count}, {_divineCount}");
        //    for (int i = 0; i < _monsterGimicList.list[_divineCount].list.Count; i++)
        //    {
        //        yield return new WaitForSeconds(_spawnTurm);

        //        Enemy selectEnemy = _monsterGimicList.list[_divineCount].list[i];
        //        selectEnemy.gameObject.SetActive(true);
        //        selectEnemy.BattleController = this;

        //        Color startColor = new Color(1, 1, 1, 0);
        //        selectEnemy.SpriteRendererCompo.color = startColor;
        //        selectEnemy.SpriteRendererCompo.DOFade(1, 0.1f);

        //        onFieldMonsterList.Add(selectEnemy);
        //    }

        //    _enemyHpBarMaker.SetupEnemyHpBar();
        //}
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
        StartCoroutine(SpawnMonster());
    }
}
