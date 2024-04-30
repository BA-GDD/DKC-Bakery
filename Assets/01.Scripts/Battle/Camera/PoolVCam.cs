using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolVCam : PoolableMono
{
    private CinemachineVirtualCamera vCam;
    private CinemachineConfiner2D confiner2D;
    public CinemachineVirtualCamera VCam { get => vCam; }
    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }
    public override void Init()
    {
        vCam.m_Follow = null;
    }
    public PoolVCam SetCamera(CinemachineSmoothPath path)
    {
        vCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = path;
        return this;
    }
    public PoolVCam SetCamera(Vector3 pos, float size = 0)
    {
        pos.z = -10;
        transform.position = pos;
        if (size < 1)
            size = Camera.main.orthographicSize;
        confiner2D.m_BoundingShape2D = GameManager.Instance.GetContent<BattleContent>()?.contentConfiner;
        confiner2D.InvalidateCache();
        vCam.m_Lens.OrthographicSize = size;

        return this;
    }
    public PoolVCam SetCameraWithClamp(Vector3 pos, float size = 0, float ratio = 0.2f)
    {
        if (size < 1)
            size = Camera.main.orthographicSize;
        confiner2D.m_BoundingShape2D = GameManager.Instance.GetContent<BattleContent>()?.contentConfiner;
        confiner2D.InvalidateCache();
        vCam.m_Lens.OrthographicSize = size;

        float x = Camera.main.aspect * size;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
         //screenPos.x * ratio;
        //screenPos.x = Mathf.Clamp(screenPos.x - x * 0.5f, Screen.width * ratio, Screen.width) + x * 0.5f;
        pos = Camera.main.ScreenToWorldPoint(screenPos);
        pos.z = -10;
        transform.position = pos;

        return this;
    }
    public void SetFollow(Transform trm)
    {
        vCam.m_Follow = trm;
    }
}
