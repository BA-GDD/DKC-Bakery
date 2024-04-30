using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionInstruction : CustomYieldInstruction
{
    private float _timer;
    private CameraMoveNode _node;
    private float _offset;

    private float _sizeOffset;

    private CinemachineVirtualCamera _vCam;

    private CinemachineTrackedDolly _trackedDolly;
    public override bool keepWaiting
    {
        get
        {
            _timer = Mathf.Clamp(_timer + Time.deltaTime,0, _node.moveNextNode);
            _trackedDolly.m_PathPosition = _offset + (_timer / _node.moveNextNode);
            _vCam.m_Lens.OrthographicSize = Mathf.Lerp(_sizeOffset,_node.orthoGraphicSize, _timer / _node.moveNextNode);
            return _node.moveNextNode <= _timer;
        }
    }
    public CameraTransitionInstruction(CinemachineTrackedDolly td, CameraMoveNode node, CinemachineVirtualCamera vCam, float offset)
    {
        _trackedDolly = td;
        _node = node;
        _vCam = vCam;
        _sizeOffset = vCam.m_Lens.OrthographicSize;
        _timer = 0;
    }
}
