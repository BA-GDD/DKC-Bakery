using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private AudioSource _audioSource;
    public AudioSource AudioSource
    {
        get
        {
            if(_audioSource != null) return _audioSource;
            _audioSource = GetComponent<AudioSource>();
            return _audioSource;
        }
    }

    [SerializeField] private BGMListSO _bgmListSO;
    public BGMListSO BgmListSO => _bgmListSO;
        
    public void PlaySFX(AudioClip sfxClip)
    {
        AudioSource.PlayOneShot(sfxClip);
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        AudioSource.clip = bgmClip;
        AudioSource.Play();
    }

    public void SetVolume(float volume)
    {
        AudioSource.volume = volume;
    }
}
