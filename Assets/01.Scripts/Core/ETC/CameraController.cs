using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoSingleton<CameraController>
{
    private CinemachineBrain brain;
    public PoolVCam cam { get; private set; }

    private void Awake()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    public void SetTransitionTime(float time)
    {
        brain.m_DefaultBlend.m_Time = time;
    }
    public PoolVCam GetVCam()
    {
        PoolVCam tempCam = null;
        if (cam != null)
            tempCam = cam;

        cam = PoolManager.Instance.Pop(PoolingType.VCamPool) as PoolVCam;
        cam.VCam.Priority = 15;

        if(tempCam != null)
            PoolManager.Instance.Push(tempCam);

        return cam;
    }

    public void SetDefaultCam()
    {
        SetTransitionTime(2);
        if(cam != null)
            PoolManager.Instance.Push(cam);
        cam = null;
    }

}
