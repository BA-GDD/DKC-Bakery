using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : SceneUI
{
    [SerializeField] private AudioClip bgmClip;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        SoundManager.PlayAudio(bgmClip, true);
    }
}
