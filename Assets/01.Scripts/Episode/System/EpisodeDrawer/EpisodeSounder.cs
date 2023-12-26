using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpisodeDialogueDefine;

public class EpisodeSounder : MonoBehaviour
{
    [SerializeField] private AudioSource _auidoSource;
    [SerializeField] private List<AudioClip> _sfxClipList = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _bgmClipList = new List<AudioClip>();

    public void HandleOutputSFX(SFXType st)
    {
        _auidoSource.PlayOneShot(_sfxClipList[(int)st]);
    }

    public void HandleChangeBGM(BGMType bg)
    {
        _auidoSource.Stop();
        _auidoSource.clip = _bgmClipList[(int)bg];
        _auidoSource.Play();
    }
}
