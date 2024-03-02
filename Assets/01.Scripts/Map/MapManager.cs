using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChapterDefine;
using DG.Tweening;

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

    public List<MapDataSO> _mapDataList = new List<MapDataSO>();
    private MapDataSO _currentChapterData;
    public StageDataSO SelectStageData { get; set; }
    private ChapterInfoPanel _chapterInfoPanel;
    [SerializeField] private StageBubble _stageBubblePrefab;
    private StageBubble _stageBubbleObject;

    public bool isOnPanel;
    public bool isOnLoadMap;

    private int _selectStageNumber;
    public int SelectStageNimber
    {
        get
        {
            return _selectStageNumber;
        }
        set
        {
            _selectStageNumber = value;
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
            if (value == ChapterType.None) return;
            _currentChapterData = _mapDataList[(int)value];
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !isOnPanel && !isOnLoadMap)
        {
            GetInfoPanel().SetInfo(_currentChapterData);
            ActiveChapterInfoPanel(true);
        }
    }

    public void ActiveChapterInfoPanel(bool isActive)
    {
        GetInfoPanel().ActivePanel(isActive);
    }

    public void ActiveLoadMapPanel(bool isActive)
    {
        GetInfoPanel().ActiveLoadMap(isActive);
    }

    public void CreateStageInfoBubble(string stageName, Vector2 pos, bool isReverse)
    {
        if(_stageBubbleObject != null)
        {
            Destroy(_stageBubbleObject.gameObject);
        }

        Transform parent = GetInfoPanel().GetNodeLoadMap().BubbleTrm;
        _stageBubbleObject = Instantiate(_stageBubblePrefab, parent);
        _stageBubbleObject.transform.localPosition = pos + new Vector2(0, 20);
        _stageBubbleObject.transform.DOLocalMove(pos, 0.3f);

        _stageBubbleObject.SetInfo(stageName, isReverse);
    }

    private ChapterInfoPanel GetInfoPanel()
    {
        if (_chapterInfoPanel != null) return _chapterInfoPanel;

        _chapterInfoPanel = FindObjectOfType<ChapterInfoPanel>();
        return _chapterInfoPanel;
    }
}
