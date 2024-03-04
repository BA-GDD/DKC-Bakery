using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDangerAttackArea : MonoBehaviour
{
    public List<AttackAreaRenderer> _areas = new List<AttackAreaRenderer>();
    private int _idx = 0;
    public void Show()
    {
        _areas[_idx].Render();
    }
    public void End()
    {
        _areas[_idx].Off();
        _idx = (_idx + 1) % _areas.Count;
    }
    public void Reset()
    {
        End();
        _idx = 0;
    }
}
