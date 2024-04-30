using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMover : MonoBehaviour
{
    public CameraTrackSO so;
    public CinemachineSmoothPath path;

    public Transform trm;

    public void TrackingCamere()
    {
    }
    public IEnumerator CameraMove()
    {
        CinemachineVirtualCamera vCam = CameraController.Instance.GetVCam(path).VCam;
        CinemachineTrackedDolly trackDolly = vCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        for (int i = 0; i < so.points.Count; i++)
        {
            if (i != 0)
            {
                yield return new CameraTransitionInstruction(trackDolly, so.points[i].node, vCam, i);
            }

            yield return so.points[i].node.GetYield();
        }
    }

    public T GetPointAtFirst<T>() where T : CameraMoveNode
    {
        foreach (var p in so.points)
        {
            if (p.GetType() == typeof(T))
                return p as T;
        }
        return null;
    }

    public CameraMoveNode GetPointAtIndex(int idx)
    {
        return so.points[idx].node;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (so != null)
        {
            if (path == null)
            {
                Debug.LogError($"{typeof(CinemachineSmoothPath)}is null");
                return;
            }
            List<CameraMoveNode> node = new();
            for (int i = 0; i < so.points.Count; i++)
            {
                if (so.points[i].isPoint)
                    node.Add(so.points[i].node);
            }
            path.m_Waypoints = new CinemachineSmoothPath.Waypoint[node.Count];
            for (int i = 0; i < node.Count; i++)
            {
                path.m_Waypoints[i] = new CinemachineSmoothPath.Waypoint()
                {
                    position = so.points[i].node.ReTurnPoint(),
                    roll = 0
                };
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        List<CameraMoveNode> node = new();
        for (int i = 0; i < so.points.Count; i++)
        {
            if (so.points[i].isPoint)
                node.Add(so.points[i].node);
        }
        for (int i = 0; i < path.m_Waypoints.Length; i++)
        {
            Vector2 pos = path.m_Waypoints[i].position + transform.position;
            float orthoSize = node[i].orthoGraphicSize;
            Vector2 screenSize = new Vector2(orthoSize * Camera.main.aspect, orthoSize);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(pos, screenSize);
        }
    }
#endif
}
