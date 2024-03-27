using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarMaker : MonoBehaviour
{
    private Transform _enemyHealthBarParent;
    [SerializeField] private EnemyHPBar _enemyHpBarPrefab;

    private List<EnemyHPBar> enemyHPBars = new();

    private void Awake()
    {
        _enemyHealthBarParent = UIManager.Instance.CanvasTrm;
        enemyHPBars = new();
    }

    public void SetupEnemysHpBar()
    {
        Enemy[] fieldInEnemys = FindObjectsOfType<Enemy>();

        foreach (Enemy e in fieldInEnemys)
        {
            SpawnHPBar(e);
        }
    }
    public void SetupEnemyHpBar(Enemy e)
    {
        SpawnHPBar(e);
    }

    public void DeleteAllHPBar()
    {
        foreach (var b in enemyHPBars)
        {
            Destroy(b.gameObject);
        }
        enemyHPBars.Clear();
    }
    private void SpawnHPBar(Enemy e)
    {
        EnemyHPBar enemyHpBar = Instantiate(_enemyHpBarPrefab, _enemyHealthBarParent);
        e.OnHealthBarChanged.AddListener(enemyHpBar.HandleHealthChanged);
        e.HealthCompo.OnDeathEvent.AddListener(() => Destroy(enemyHpBar.gameObject));
        e.HealthCompo.OnBeforeHit += () => FeedbackManager.Instance.FreezeTime(0.8f, 0.2f);

        enemyHpBar.OwnerOfThisHpBar = e.hpBarPos;

        enemyHPBars.Add(enemyHpBar);
    }
}
