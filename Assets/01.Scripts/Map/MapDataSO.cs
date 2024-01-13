using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChapterDefine;

[CreateAssetMenu(menuName = "SO/MapData")]
public class MapDataSO : ScriptableObject
{
    public ChapterType myChapterType;
    public Sprite chapterSprite;
    public NodeLaodMap loadMap;
    public string chapterName;
    public string chapterInfo;
}
