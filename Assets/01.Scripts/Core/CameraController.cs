using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoSingleton<CameraController>
{

    [SerializeField] private CinemachineVirtualCamera _defaultCVCam;
    [SerializeField] private CinemachineVirtualCamera _followCVCam;

    public void SetDefaultCam()
    {
        _defaultCVCam.Priority = 20;
        _followCVCam.Priority = 10;
    }
    public void SetFollowCam(Transform followObj,Transform lookObj)
    {
        _defaultCVCam.Priority = 10;
        _followCVCam.Priority = 20;

        _followCVCam.Follow = followObj;
        _followCVCam.LookAt = lookObj;
    }
}
