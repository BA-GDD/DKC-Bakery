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
    public List<Enemy> DeathEnemyList { get; private set; } = new List<Enemy>();
    public List<Enemy> SpawnEnemyList { get; private set; } = new List<Enemy>();


    [HideInInspector]
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
    private bool _isGameEnd;
    public bool IsGameEnd
    {
        get => _isGameEnd;
        set
        {
            _isGameEnd = value;
            if (_isGameEnd)
            {
                foreach (var e in onFieldMonsterList)
                {
                    if (e == null) continue;
                    e.turnStatus = TurnStatus.End;
                }
                _enemyHpBarMaker.DeleteAllHPBar();
            }
        }
    }

    [SerializeField] private UnityEvent<Entity> OnChangePlayerTarget;

    private void Awake()
    {
        //_enemyHpBarMaker = FindObjectOfType<EnemyHpBarMaker>();



    }
    private void Start()
    {
        _enemyHpBarMaker = FindObjectOfType<EnemyHpBarMaker>();

        onFieldMonsterList = new Enemy[_spawnDistanceByPoint.Count];

        TurnCounter.PlayerTurnStartEvent += HandleCardDraw;
        TurnCounter.EnemyTurnStartEvent += OnEnemyTurnStart;
        TurnCounter.EnemyTurnEndEvent += OnEnemyTurnEnd;

        Player.BattleController = this;
        Player.HealthCompo.OnDeathEvent.AddListener(() => IsGameEnd = true);
    }

    private void HandleCardDraw(bool obj)
    {
        CardReader.CardDrawer.DrawCard(1, false);
    }

    private void OnDestroy()
    {
        TurnCounter.EnemyTurnStartEvent -= OnEnemyTurnStart;
        TurnCounter.EnemyTurnEndEvent -= OnEnemyTurnEnd;
        TurnCounter.PlayerTurnStartEvent -= HandleCardDraw;
    }

    private void OnEnemyTurnStart(bool b)
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

            if (_isGameEnd)
                break;

            yield return new WaitForSeconds(1.5f);
        }
        if (!_isGameEnd)
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
        if (Player.target == null)
            SetPlayerCloseTarget();
        //_enemyHpBarMaker.SetupEnemyHpBar();
    }
    private void SpawnMonster(int idx)
    {
        if (_enemyQue.Count > 0)
        {
            Vector3 pos = _spawnDistanceByPoint[idx].position;
            Enemy selectEnemy = PoolManager.Instance.Pop(_enemyQue.Dequeue()) as Enemy;
            _enemyHpBarMaker.SetupEnemyHpBar(selectEnemy);
            selectEnemy.transform.position = pos;
            selectEnemy.BattleController = this;
            int posChecker = ((idx + 3) % 2) * 2;
            selectEnemy.SpriteRendererCompo.sortingOrder = posChecker;

            selectEnemy.HealthCompo.OnDeathEvent.AddListener(() => DeadMonster(selectEnemy));

            onFieldMonsterList[idx] = selectEnemy;
            selectEnemy.Spawn(pos);
            selectEnemy.target = Player;

            SpawnEnemyList.Add(selectEnemy);
        }
    }

    public void DeadMonster(Enemy enemy)
    {
        onFieldMonsterList[Array.IndexOf(onFieldMonsterList, enemy)] = null;
        if (enemy == Player.target)
            SetPlayerCloseTarget();
        DeathEnemyList.Add(enemy);
    }
    private void SetPlayerCloseTarget()
    {
        for (int i = onFieldMonsterList.Length - 1; i >= 0; i--)
        {
            Enemy e = onFieldMonsterList[i];

            if (e != null && !e.HealthCompo.IsDead)
            {
                ChangePlayerTarget(e);
                return;
            }
        }
        ChangePlayerTarget(null);
    }
    public bool IsStuck(int to, int who)
    {
        return isStuck.list[to].list[who];
    }

    public void ChangePosition(Transform e1, Transform e2, Action callback = null)
    {
        e1.DOMove(e2.position, 0.5f);
        e2.DOMove(e1.position, 0.5f).OnComplete(() => callback?.Invoke());
    }

    public void ChangePlayerTarget(Entity entity)
    {
        Player.target = entity;
        OnChangePlayerTarget?.Invoke(entity);
    }
}
