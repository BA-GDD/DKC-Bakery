using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public struct CamMoveTrack
{
    public Vector3 pos;
    public float duration;
}

public class CameraMoveTrack : MonoBehaviour
{
    public List<CamMoveTrack> camMoves;
    public Transform targetTrm;

    private void Start()
    {
        targetTrm.position = transform.position = Camera.main.transform.position;
    }

    public void StartMove()
    {
        Sequence seq = DOTween.Sequence();
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
