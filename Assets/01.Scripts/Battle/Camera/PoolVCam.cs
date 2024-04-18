using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolVCam : PoolableMono
{
    private CinemachineVirtualCamera vCam;
    public CinemachineVirtualCamera VCam { get => vCam; }
    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }
    public override void Init()
    {
    }
    public void SetCamera(Vector3 pos ,float size = 0)
    {
        pos.z = -10;
        transform.position = pos;
        if (size < 1)
            size = Camera.main.orthographicSize;
        vCam.m_Lens.OrthographicSize = size;

    }
    public void SetFollow(Transform trm)
    {
        vCam.m_Follow = trm;
    }
}
