using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsumegoSystem : MonoBehaviour
{
    public TsumegoInfo CurTsumegoInfo;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CheckClear();
        }
    }

    public void CheckClear()
    {
        foreach(var condition in CurTsumegoInfo.Conditions)
        {
            if (!condition.CheckCondition())
            {
                // ����
                Debug.Log("����");
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
        Debug.Log("����");

        // Ŭ���� ����, ���� ����, Ŭ���� ������ ���� ó��
    }
}
