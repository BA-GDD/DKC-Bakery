using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSetter : MonoBehaviour
{
    [SerializeField] private Transform _stageParent;
    private void OnEnable()
    {
        Debug.Log(MapManager.Instanace.SelectStageData.stagePrefab);
        Instantiate(MapManager.Instanace.SelectStageData.stagePrefab, _stageParent);
    }
}
