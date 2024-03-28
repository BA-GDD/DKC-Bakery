using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TsumegoSystem : MonoBehaviour
{
    [SerializeField] private UnityEvent _stageClearEvent;
    [SerializeField] private UnityEvent<bool> _gameEndEvent; 
    public TsumegoInfo CurTsumegoInfo { get; set; }

    public void CheckClear()
    {
        Debug.Log(1);
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
        Debug.Log(1);
        CurTsumegoInfo.IsClear = true;
        _gameEndEvent?.Invoke(true);
        _stageClearEvent?.Invoke();
        // Ŭ���� ����, ���� ����, Ŭ���� ������ ���� ó��
    }
}
