using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerObject : MonoBehaviour
{
    public bool playerIsInTriggered = false;
    public GameObject effect;

    private Stage _stage;

    private void Awake()
    {
        _stage = FindObjectOfType<Stage>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInTriggered = true;
            _stage.curPhaseCleared = true;
        }
    }
}
