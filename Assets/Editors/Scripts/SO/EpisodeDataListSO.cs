using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Episode/EpisodeDataList")]
public class EpisodeDataListSO : ScriptableObject
{
    public List<EpisodeData> episodeDataList = new List<EpisodeData>();
}
