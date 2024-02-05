using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Stage : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    private List<Transform> _stageTrms;
    public int maximumPhase = 3;//기본값 3 

    [HideInInspector]
    public bool curPhaseCleared = false;    

    public Action OnStageStarted = null;
    public Action OnPhaseCleared = null;
    public Action OnStageCleared = null;

    private float halfHeight = 0;
    private float halfWidth = 0;

    private BoxCollider2D[] stageCollider = {null, null};

    protected int curPhase = 0;

    protected virtual void Awake()
    {
        _stageTrms = new List<Transform>();

         halfHeight = Camera.main.orthographicSize;
         halfWidth= Camera.main.aspect * halfHeight;

        OnStageCleared += Print;

        for(int i = 0; i < 2; i++)
        {
            GameObject obj = new GameObject($"mapCollider_{i}");

            obj.transform.localScale = new Vector3(1, 20,1);
            
            stageCollider[i] = obj.AddComponent<BoxCollider2D>();

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
        CamPosGenerate();
        OnStageStarted?.Invoke();
    }

    private void Update()
    {
        //디버그 코드
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            PhaseCleared();
        }
    }

    /// <summary>
    /// 카메라 크기 만큼 이동하는 거?
    /// </summary>
    private void CamPosGenerate()
    {
        for(int i = 0; i < maximumPhase; ++i)
        {
            Transform trm = new GameObject().transform;
            trm.name = $"camTrm_{i + 1}";
            trm.position = new Vector3(i * (halfWidth*2.0f), 0, 0);
            _stageTrms.Add(trm);
        }

        vCam.m_Follow = _stageTrms[curPhase];
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
            vCam.m_Follow = _stageTrms[curPhase];
        }
        curPhaseCleared = false;
    }

}
