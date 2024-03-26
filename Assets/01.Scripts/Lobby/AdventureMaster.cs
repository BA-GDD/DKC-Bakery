using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct MissionPanel
{
    public TextMeshProUGUI clearCountTxt;
}

[Serializable]
public struct MinePanel
{
    public TextMeshProUGUI clearCountTxt;
}

[Serializable]
public struct StagePanel
{
    public TextMeshProUGUI inStageCount;
    public List<Sprite> chapterVisualList;
    public List<GameObject> chapterVisualVFXList;
    public Image visual;
}

public class AdventureMaster : MonoBehaviour
{
    private AdventureData _adventureData = new AdventureData();
    private const string _adventureKey = "AdventureKEY";

    [SerializeField] private MissionPanel _missionPanel = new MissionPanel();
    [SerializeField] private MinePanel _minePanel = new MinePanel();
    [SerializeField] private StagePanel _stagePanel = new StagePanel();
    private GameObject _currentChapterVFX;

    private void Start()
    {
        if (DataManager.Instance.IsHaveData(_adventureKey))
        {
            _adventureData = DataManager.Instance.LoadData<AdventureData>(_adventureKey);
        }

        _missionPanel.clearCountTxt.text = $"In Stage : {_adventureData.InChallingingMissionName}";
        _minePanel.clearCountTxt.text = $"Conquered Floor : {_adventureData.ClearMineFloor}";
        _stagePanel.inStageCount.text = $"Challinging Area : {_adventureData.InChallingingStageCount}";

        int idx = Convert.ToInt16(_adventureData.InChallingingStageCount.Split('-')[0]);
        _stagePanel.visual.sprite = _stagePanel.chapterVisualList[idx - 1];

        _currentChapterVFX.SetActive(false);
        _currentChapterVFX = _stagePanel.chapterVisualVFXList[idx - 1];
        _currentChapterVFX.SetActive(true);
    }
}
