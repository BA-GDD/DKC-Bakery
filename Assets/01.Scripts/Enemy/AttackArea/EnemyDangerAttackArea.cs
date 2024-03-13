using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackAreaArray
{
    public List<AttackAreaRenderer> _areas;
    public void Show()
    {
        foreach (var r in _areas)
        {
            r.Render();
            Debug.Log("?");
        }
    }
    public void Off()
    {
        foreach (var r in _areas)
        {
            r.Off();
        }
    }
}
public class EnemyDangerAttackArea : MonoBehaviour
{

    public List<AttackAreaArray> areaArrays = new List<AttackAreaArray>();
    private int _idx = 0;
    public void Show()
    {
        Debug.Log("?");
        areaArrays[_idx].Show();
    }
    public void End()
    {

        areaArrays[_idx].Off();
        _idx = (_idx + 1) % areaArrays.Count;
    }
    public void Reset()
    {
        End();
        _idx = 0;
    }
}
