using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class MonsterGimic
{
    public Enemy enemy;
    public Transform appearTrm;
}

[Serializable]
public class SEList<T>
{
    public List<T> list;
}

[Serializable]
public class MonsterGimicList : SEList<SEList<MonsterGimic>>
{

}

public class BattleController : MonoBehaviour
{
    [Header("페이지에 따른 몬스터 출현 기믹")]
    [SerializeField] private MonsterGimicList _monsterGimicList = new();
    private ExpansionList<Enemy> _onFieldMonsterList = new ExpansionList<Enemy>();
    private Stage _currentStage;
    private int _phaseDevineIdx;

    [Header("출현 텀")]
    [SerializeField][Range(0.01f, 0.1f)] private float _spawnTurm;

    private void Start()
    {
        _currentStage = FindObjectOfType<Stage>();
        _onFieldMonsterList.ListChanged += HandleChangeMonsterCountOnField;

        _currentStage.OnPhaseCleared += () => StartCoroutine(SpawnMonster());
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        _currentStage.CanPhseCleard = false;
        yield return null;

        for(int i = 0; i < _monsterGimicList.list[_currentStage.CurPhase].list.Count; i++)
        {
            yield return new WaitForSeconds(_spawnTurm);

            MonsterGimic mg = _monsterGimicList.list[_currentStage.CurPhase].list[i];

            Enemy enemy = Instantiate(mg.enemy, mg.appearTrm.position, Quaternion.identity);
            _onFieldMonsterList.Add(enemy);

            enemy.SpriteRendererCompo.color = new Color(1, 1, 1, 0);
            enemy.SpriteRendererCompo.DOFade(1, 0.1f);
        }
    }

    private void HandleChangeMonsterCountOnField(object sender, EventArgs e)
    {
        if(_onFieldMonsterList.Count == 0)
        {
            _phaseDevineIdx++;

            if (_phaseDevineIdx !=
               _monsterGimicList.list[_currentStage.CurPhase].list.Count)
            {
                StartCoroutine(SpawnMonster());
            }
            else
            {
                _currentStage.CanPhseCleard = true;
            }
        }
    }
}
