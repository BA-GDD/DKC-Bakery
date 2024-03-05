using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/StageData")]
public class StageDataSO : ScriptableObject
{
    public string stageName;
    public GameObject stagePrefab;
}
