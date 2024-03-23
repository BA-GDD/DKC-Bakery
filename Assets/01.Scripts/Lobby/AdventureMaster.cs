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
    public Image visual;
}

public class AdventureMaster : MonoBehaviour
{
    private AdventureData _adventureData = new AdventureData();
    private const string _adventureKey = "AdventureKEY";

    [SerializeField] private MissionPanel _missionPanel = new MissionPanel();
    [SerializeField] private MinePanel _minePanel = new MinePanel();
    [SerializeField] private StagePanel _stagePanel = new StagePanel();

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(_adventureKey))
        {
            _adventureData = DataManager.Instance.LoadData<AdventureData>(_adventureKey);
        }

        _missionPanel.clearCountTxt.text = _adventureData.ClearMissionCount;
        _minePanel.clearCountTxt.text = _adventureData.ClearMineFloor;
        _stagePanel.inStageCount.text = _adventureData.InChallingingStageCount;

        int idx = Convert.ToInt16(_adventureData.InChallingingStageCount.Split('_'));
        _stagePanel.visual.sprite = _stagePanel.chapterVisualList[idx];
    }
}
