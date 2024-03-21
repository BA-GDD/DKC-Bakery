using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsumegoSystem : MonoBehaviour
{
    public TsumegoInfo CurTsumegoInfo { get; set; }

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
                Debug.Log("실패");
                return;
            }
        }
        ClearStage();
    }

    public void ClearStage()
    {
        // SO에 클리어 처리
        CurTsumegoInfo.IsClear = true;
        Debug.Log("성공");

        // 클리어 연출, 보상 지급, 클리어 데이터 갱신 처리
    }
}
