using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EpisodeSelecter : MonoBehaviour
{
    [SerializeField] private EpisodeData[] _episodeDataArr = new EpisodeData[10];
    [SerializeField] private UnityEvent<EpisodeData> _episodeStartEvent;

    private void OnEnable()
    {
        EpisodeData selectData = _episodeDataArr[MawangManager.Instanace.currentLikeability - 1];
        _episodeStartEvent?.Invoke(selectData);
    }
}
