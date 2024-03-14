using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoSingleton<CameraController>
{

    public CinemachineVirtualCamera _defaultCVCam;
    public CinemachineVirtualCamera _followCVCam;

    private Entity _followOwner;
    private CinemachineTrackedDolly _dolly;
    public float DollyPos
    {
        get
        {
            return _dolly.m_PathPosition;
        }
        set
        {
            _dolly.m_PathPosition = value;
        }
    }
    private bool _isFollow;

    private void Start()
    {
        _dolly = _followCVCam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    public void SetDefaultCam()
    {
        _defaultCVCam.Priority = 20;
        _followCVCam.Priority = 10;

        _isFollow = false;
    }
    public void SetFollowCam(Entity entity, CinemachinePathBase followPath, Transform lookObj)
    {
        _dolly.m_PathPosition = 0;

        _followOwner = entity;
        _dolly.m_Path = followPath;
        _followCVCam.LookAt = lookObj;

        _defaultCVCam.Priority = 10;
        _followCVCam.Priority = 20;

        _isFollow = true;
    }
}
