using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : SceneUI
{
    [SerializeField] private AudioClip bgmAudio;

    private void OnEnable()
    {
        StartCoroutine(SoundPlay());
    }

    private IEnumerator SoundPlay()
    {
        yield return new WaitForSeconds(1.0f);
        SoundManager.PlayAudio(bgmAudio, true);
    }
}
