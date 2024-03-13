using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoSingleton<CameraController>
{

    [SerializeField] private CinemachineVirtualCamera _defaultCVCam;
    [SerializeField] private CinemachineVirtualCamera _followCVCam;

    [SerializeField]private Transform _followObj;
    private Transform _left, _right;
    private bool _isFollow;

    public void SetDefaultCam()
    {
        _defaultCVCam.Priority = 20;
        _followCVCam.Priority = 10;
        _isFollow = false;
    }
    public void SetFollowCam(Transform left, Transform right)
    {
        _defaultCVCam.Priority = 10;
        _followCVCam.Priority = 20;


        _left = left;
        _right = right;
        _isFollow = true;
    }
    private void Update()
    {
        if(_isFollow)
        {
            float x = _left.transform.position.x + (_right.position.x - _left.position.x) * 0.5f;
            float y = _right.position.y;
            _followObj.transform.position = new Vector2(x, y);
            float dis = Vector2.Distance(_right.transform.position, _left.transform.position);
            _followCVCam.m_Lens.OrthographicSize = Mathf.Clamp(dis * 9f / 16f,4,8);
        }
    }
}
