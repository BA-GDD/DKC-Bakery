using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Stage : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public List<Transform> stageTrms;

    public Action OnStageStarted = null;
    public Action OnPhaseCleared = null;
    public Action OnStageCleared = null;

    private float halfHeight = 0;
    private float halfWidth = 0;

    public int maximumPhase = 3;//기본값 3 
    private int _curPhase = 0;

    private void Awake()
    {
         halfHeight = Camera.main.orthographicSize;
         halfWidth= Camera.main.aspect * halfHeight;

        OnStageCleared += Print;
    }

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

    private void CamPosGenerate()
    {
        for(int i = 0; i < maximumPhase; ++i)
        {
            Transform trm = new GameObject().transform;
            trm.name = $"camTrm_{i + 1}";
            trm.position = new Vector3(i * halfWidth, 0, 0);
            stageTrms.Add(trm);
        }

        vCam.m_Follow = stageTrms[_curPhase];
    }

    public void PhaseCleared()
    {
        if(_curPhase >= maximumPhase)
        {
            OnStageCleared?.Invoke();
            return;
        }

        OnPhaseCleared?.Invoke();
        _curPhase++;

        if(_curPhase < maximumPhase)
        {
            vCam.m_Follow = stageTrms[_curPhase];
        }
    }
}
