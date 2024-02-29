using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Stage : MonoBehaviour
{
    public int stageIndex = 0;
    public CinemachineVirtualCamera vCam;
    public StageInfoSO stageInfo;
    
    private List<Transform> _stageInfo;
    public int maximumPhase = 3;//기본값 3 

    public Transform camTrmsParent;

    [HideInInspector]
    public bool curPhaseCleared = false;    

    public Action OnStageStarted = null;
    public Action OnPhaseCleared = null;
    public Action OnStageCleared = null;

    private static float halfHeight = 0;
    private static float halfWidth = 0;

    private BoxCollider2D[] stageCollider = {null, null};

    public int curPhase = 0;

    protected virtual void Awake()
    {
        _stageInfo = new List<Transform>();

         halfHeight = Camera.main.orthographicSize;
         halfWidth = Mathf.Ceil(Camera.main.aspect * halfHeight);

        OnStageCleared += Print;

        for(int i = 0; i < 2; i++)
        {
            GameObject obj = new GameObject($"mapCollider_{i}");

            obj.transform.localScale = new Vector3(1, 20,1);
            
            stageCollider[i] = obj.AddComponent<BoxCollider2D>();

            Debug.Log(halfWidth);
            obj.transform.position = new Vector2((halfWidth * 2) * i - halfWidth, 0);
            obj.transform.SetParent(vCam.transform, false);
        }

        stageCollider[1].gameObject.AddComponent<PhaseMove>();


    }

    //debug
    private void Print()
    {
        print("stage Cleared!");
    }

    private void Start()
    {
        stageInfo.GetList();

        StageInfoGenerate();
        OnStageStarted?.Invoke();
    }

    /// <summary>
    /// 카메라 크기 만큼 이동하는 거?
    /// </summary>
    private void StageInfoGenerate()
    {
        for(int i = 0; i < maximumPhase; ++i)
        {
            if(int.Parse(stageInfo.datas[stageIndex].str[i]) == 0)
            {
                Transform trm = new GameObject().transform;
                trm.name = $"camTrm_{i + 1}";
                trm.position = new Vector3(i * (halfWidth * 2.0f), 0, 0);
                trm.SetParent(camTrmsParent);
                _stageInfo.Add(trm);
            }
            else
            {
                _stageInfo.Add(GameManager.Instance.PlayerTrm);
            }

            ConfinerGenerate($"confiner{i}", new Vector2(halfWidth, halfHeight), new Vector2(i * (halfWidth * 2.0f), 0));
        }

        vCam.m_Follow = _stageInfo[curPhase];
    }

    private void ConfinerGenerate(string objName, Vector2 size, Vector2 pos)
    {
        GameObject obj = new GameObject();
        obj.name = objName;
        obj.transform.position = pos;
        BoxCollider2D col = obj.AddComponent<BoxCollider2D>();
        col.isTrigger = true;

        col.size = size;
    }

    /// <summary>
    /// 한 페이즈가 끝났을 때에 실행하는 거
    /// </summary>
    public void PhaseCleared()
    {
        if(curPhase >= maximumPhase)
        {
            OnStageCleared?.Invoke();
            return;
        }

        OnPhaseCleared?.Invoke();
        curPhase++;

        if(curPhase < maximumPhase)
        {
            vCam.m_Follow = _stageInfo[curPhase];
        }
        curPhaseCleared = false;
    }

}
