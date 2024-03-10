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
    [SerializeField][Range(0.01f, 0.1f)] private float _spawnTurm;

    private void Awake()
    {
        _enemyHpBarMaker = FindObjectOfType<EnemyHpBarMaker>();
    }

    private void Start()
    {
        for (int i = 0; i < _monsterGimicList.list.Count; i++)
        {
            for (int j = 0; j < _monsterGimicList.list[i].list.Count; j++)
            {
                _monsterGimicList.list[i].list[j].gameObject.SetActive(false);
            }
        }

        _currentStage = FindObjectOfType<Stage>();
        onFieldMonsterList.ListChanged += HandleChangeMonsterCountOnField;

        _currentStage.OnPhaseCleared += () => StartCoroutine(SpawnMonster());
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        yield return null;
        if(_monsterGimicList.list.Count >= _divineCount)
        {
            Debug.Log($"{_monsterGimicList.list.Count}, {_divineCount}");
            for (int i = 0; i < _monsterGimicList.list[_divineCount].list.Count; i++)
            {
                yield return new WaitForSeconds(_spawnTurm);

                Enemy selectEnemy = _monsterGimicList.list[_divineCount].list[i];
                selectEnemy.gameObject.SetActive(true);
                selectEnemy.BattleController = this;

                Color startColor = new Color(1, 1, 1, 0);
                selectEnemy.SpriteRendererCompo.color = startColor;
                selectEnemy.SpriteRendererCompo.DOFade(1, 0.1f);

                onFieldMonsterList.Add(selectEnemy);
            }

            _enemyHpBarMaker.SetupEnemyHpBar();
        }
    }

    private void HandleChangeMonsterCountOnField(object sender, EventArgs e)
    {
        if(onFieldMonsterList.Count == 0)
        {
            _divineCount++;

            if ((_divineCount % _divineToken) == 0)
            {
                Debug.Log("Phase Clear");
                _currentStage.CurPhaseCleared = true;
                StartCoroutine(SpawnMonster());
            }

            StartCoroutine(SpawnMonster());
        }
    }
}
