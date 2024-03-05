using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarMaker : MonoBehaviour
{
    private Transform _enemyHealthBarParent;
    [SerializeField] private EnemyHPBar _enemyHpBarPrefab;

    private void Awake()
    {
        _enemyHealthBarParent = UIManager.Instance.CanvasTrm;
    }

    public void SetupEnemyHpBar()
    {
        Enemy[] fieldInEnemys = FindObjectsOfType<Enemy>();

        foreach(Enemy e in fieldInEnemys)
        {
            EnemyHPBar enemyHpBar = Instantiate(_enemyHpBarPrefab, _enemyHealthBarParent);
            e.OnHealthBarChanged.AddListener(enemyHpBar.HandleHealthChanged);
            e.OnDieEvent.AddListener(() => Destroy(enemyHpBar));
            e.OnBeforeHit += () => FeedbackManager.Instance.FreezeTime(0.8f, 0.2f);

            enemyHpBar.OwnerOfThisHpBar = e.hpBarPos;
        }
    }
}
