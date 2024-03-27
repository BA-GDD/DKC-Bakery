using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonFireUIFX : MonoBehaviour
{
    private RectTransform rtTrm;

    private void Awake()
    {
        rtTrm = GetComponent<RectTransform>();
    }

    public void Play()
    {
        rtTrm.DOScaleY(1, 0.1f);
    }

    public void Stop()
    {
        rtTrm.DOScaleY(0, 0.1f);
    }

    //DEBUG
    /*private void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Play();
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            Stop();
        }
    }*/
}
