using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleProduction : MonoBehaviour
{
    [SerializeField] private Animator _productionAnimator;
    [SerializeField] private UnityEvent<EnemyGroupSO, string> _panelSetEvent;
    [SerializeField] private UnityEvent _battleStartEvent;

    private void Start()
    {
        StartCoroutine(ProductionCo());
    }

    protected IEnumerator ProductionCo()
    {
        _panelSetEvent?.Invoke(MapManager.Instanace.SelectStageData.enemyGroup, "???");
        _productionAnimator.SetBool("isOnProduction", true);
        yield return new WaitForSeconds(1.5f);
        _productionAnimator.SetBool("isOnProduction", false);
        yield return new WaitForSeconds(2f);
        _battleStartEvent?.Invoke();
    }
}
