using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TeaTimeUI : SceneUI
{
    [SerializeField]
    private PlayableDirector director;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            director.Play();
        }
    }
}
