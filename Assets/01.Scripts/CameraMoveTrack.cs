using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CardDefine;

[System.Serializable]
public struct CamMovePoint
{
    public Vector3 pos;
    public float duration;
}

public class CameraMoveTrack : MonoBehaviour
{
    public PlayerSkill skillType;

    public List<CamMovePoint> camMoves;
    public Transform targetTrm;
    private Sequence seq;

    public void StartMove()
    {
        targetTrm.position = transform.position = Camera.main.transform.position;

        if(seq != null && seq.IsPlaying())
        {
            seq.Kill();
        }
        seq = DOTween.Sequence();
        Vector3 pos = transform.position;
        foreach (var m in camMoves)
        {
            seq.Append(targetTrm.DOMove(pos + m.pos, m.duration));
            pos += m.pos;
        }
        seq.OnComplete(() => targetTrm.position = transform.position);
    }
    private void OnDrawGizmos()
    {
        if (camMoves.Count < 1) return;

        Vector3 pos = transform.position; 
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + camMoves[0].pos);
        pos += camMoves[0].pos;
        for (int i = 1; i < camMoves.Count; i++)
        {

            Gizmos.DrawLine(pos, pos + camMoves[i].pos);
            pos += camMoves[i].pos;
        }
    }
}
