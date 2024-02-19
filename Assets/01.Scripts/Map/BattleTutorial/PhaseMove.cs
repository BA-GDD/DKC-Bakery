using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseMove : MonoBehaviour
{
    private Stage _curStage = null;

    private void Start()
    {
        _curStage = FindObjectOfType<Stage>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_curStage.curPhaseCleared && collision.collider.CompareTag("Player"))
        {
            if(_curStage.TryGetComponent<BattleTutorial>(out BattleTutorial b))
            {
                _curStage.PhaseCleared();
                b.QuaterEnd("Quater End");
                GameManager.Instance.PlayerTrm.position += Vector3.right * 2;
            }
            else
            {
                _curStage.PhaseCleared();
            }
        }
        else
        {
            return;
        }
    }
}
