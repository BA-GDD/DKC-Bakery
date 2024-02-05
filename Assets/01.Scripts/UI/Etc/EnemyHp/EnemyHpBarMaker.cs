using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarMaker : MonoBehaviour
{
    [SerializeField] private Transform _enemyHealthBarParent;
    [SerializeField] private EnemyHPBar _enemyHpBarPrefab;

    private void Start()
    {
        SetupEnemyHpBar();
    }

    public void SetupEnemyHpBar()
    {
        Enemy[] fieldInEnemys = FindObjectsOfType<Enemy>();

        foreach(Enemy e in fieldInEnemys)
        {
            EnemyHPBar enemyHpBar = Instantiate(_enemyHpBarPrefab, _enemyHealthBarParent);
            e.OnHealthBarChanged.AddListener(enemyHpBar.HandleHealthChanged);

            enemyHpBar.OwnerOfThisHpBar = e.transform;
        }
    }
}
