using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChapterDefine;

[CreateAssetMenu(menuName = "SO/MapData")]
public class MapDataSO : ScriptableObject
{
    [SerializeField] private ChapterType _myChapterType;
    public ChapterType MyChapterType => _myChapterType;

    [SerializeField] private string _chapterName;
    public string ChapterName => _chapterName;

    [SerializeField] private string _chapterInfo;
    public string ChapterInfo => _chapterInfo;

    
    //[SerializeField] private List<MonsterType> _appearMonsterList = new List<MonsterType>();
    //public List<MonsterType> AppearMonsterList => _appearMonsterList;

    //[SerializeField] private List<IngredientType> _canEarnIngredientType = new List<IngredientType>();
    //public List<IngredientType> CanEarnIngredientType => _canEarnIngredientType;
}
