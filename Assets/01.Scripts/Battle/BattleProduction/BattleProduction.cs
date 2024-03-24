using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleProduction : MonoBehaviour
{
    [SerializeField] private StageInfoPanel _stageInfoPanel;
    [SerializeField] private UnityEvent<TsumegoInfo> _clearChekcerSetEvent;
    [SerializeField] private UnityEvent<StageDataSO> _panelSetEvent;
    [SerializeField] private UnityEvent _battleStartEvent;

    private void Start()
    {
        StartCoroutine(ProductionCo());
    }

    protected IEnumerator ProductionCo()
    {
        _panelSetEvent?.Invoke(MapManager.Instanace.SelectStageData);

        _stageInfoPanel.PanelSetUp();
        yield return new WaitForSeconds(1.5f);
        _stageInfoPanel.PanelInit();
        yield return new WaitForSeconds(0.5f);
        _battleStartEvent?.Invoke();
        _clearChekcerSetEvent?.Invoke(MapManager.Instanace.SelectStageData.clearCondition);
    }
}
