using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDefine;

public class PlayerCameraMoveDic : MonoSingleton<PlayerCameraMoveDic>
{
    private Dictionary<PlayerSkill, CameraMoveTrack> trackDic = new();

    private void Awake()
    {
        foreach(var t in GetComponentsInChildren<CameraMoveTrack>())
        {
            trackDic.Add(t.skillType,t);
        }
    }

    public CameraMoveTrack this[PlayerSkill str]
    {
        get { return trackDic[str]; }
    }
}
