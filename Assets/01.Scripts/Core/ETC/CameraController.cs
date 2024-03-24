using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoSingleton<CameraController>
{

    public CinemachineVirtualCamera _defaultCVCam;
    public CinemachineVirtualCamera _followCVCam;

    public void SetDefaultCam()
    {
        _defaultCVCam.Priority = 20;
        _followCVCam.Priority = 10;

    }
    public void SetFollowCam(Transform _followTrm, Transform _lookTrm)
    {
        _defaultCVCam.Priority = 10;
        _followCVCam.Priority = 20;

        _followCVCam.m_Follow = _followTrm;
        _followCVCam.m_LookAt = _lookTrm;
    }
}
