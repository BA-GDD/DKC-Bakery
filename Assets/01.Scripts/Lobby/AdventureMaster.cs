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
    public TextMeshProUGUI inMissionNameTxt;
}

[Serializable]
public struct MinePanel
{
    public TextMeshProUGUI clearCountTxt;
    public TextMeshProUGUI inMineNameTxt;
}

[Serializable]
public struct StagePanel
{
    public TextMeshProUGUI inChapterName; 
    public Image inChapterImg;
    public TextMeshProUGUI inStageCount;
    public TextMeshProUGUI inStageName;
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

        _missionPanel.clearCountTxt.text = _adventureData.ClearMissionCount.ToString();
        _missionPanel.inMissionNameTxt.text = _adventureData.InChallingingMissionName;

        _minePanel.clearCountTxt.text = _adventureData.ClearMineCount.ToString();
        _minePanel.inMineNameTxt.text = _adventureData.InChallingingMineName;

        _stagePanel.inChapterName.text = _adventureData.InChapterName;
        _stagePanel.inChapterImg.sprite = _adventureData.InChapterImg;
        _stagePanel.inStageCount.text = _adventureData.InChallingingStageCount;
        _stagePanel.inStageName.text = _adventureData.InChallingingStageName;
    }
}
