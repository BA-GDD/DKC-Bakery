using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TsumegoSystem : MonoBehaviour
{
    [SerializeField] private UnityEvent _stageClearEvent;
    public TsumegoInfo CurTsumegoInfo { get; set; }

    public void CheckClear()
    {
        foreach(var condition in CurTsumegoInfo.Conditions)
        {
            if (!condition.CheckCondition())
            {
                // ����
                return;
            }
        }
        // ���� ���� �����
        ClearStage();
    }

    public void ClearStage()
    {
        // SO�� Ŭ���� ó��
        CurTsumegoInfo.IsClear = true;
        Debug.Log(1);
        _stageClearEvent?.Invoke();
        // Ŭ���� ����, ���� ����, Ŭ���� ������ ���� ó��
    }
}
