using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonFireUIFX : MonoBehaviour
{
    private RectTransform rtTrm;
    private ParticleSystem _fx;

    private void Awake()
    {
        TryGetComponent<ParticleSystem>(out _fx);
        rtTrm = GetComponent<RectTransform>();
    }

    public void Play()
    {
        if(_fx != null) _fx.Play();
        rtTrm.DOScaleY(1, 0.1f);
    }

    public void Stop()
    {
        if (_fx != null) _fx.Play();
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
