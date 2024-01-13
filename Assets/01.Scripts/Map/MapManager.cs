using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChapterDefine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instanace
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<MapManager>();
            if (_instance == null)
            {
                Debug.LogError("Not Exist GameManager");
            }
            return _instance;
        }
    }

    private ChapterType _currentChapter;
    public ChapterType CurrentChapter
    {
        get
        {
            return _currentChapter;
        }
        set
        {
            _currentChapter = value;
        }
    }
}
