using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Main,
    Mine,
    Mission
}

public enum StageBackGround
{
    Forest,
    Dungeon,
    Myosu
}

[CreateAssetMenu(menuName ="SO/StageData")]
public class StageDataSO : ScriptableObject
{
    public string stageNumber;
    public string stageName;
    public StageType stageType;
    public StageBackGround stageBackGround;
    public EnemyGroupSO enemyGroup;
    public TsumegoInfo clearCondition;
    public List<CardBase> missionDeck;

    private const string _dataKey = "AdventureKEY";

    public void StageClear()
    {
        string[] numArr = stageNumber.Split('-');

        int chapteridx = Convert.ToInt16(numArr[0]);
        int stageidx = Convert.ToInt16(numArr[1]);

        AdventureData ad = DataManager.Instance.LoadData<AdventureData>(_dataKey);
        ad.canChallingeChapterArr[chapteridx][stageidx] = true;
        if(stageidx == 6)
        {
            ad.canChallingeChapterArr[Mathf.Clamp(chapteridx + 1, 1, 6)][stageidx] = true;
        }
        DataManager.Instance.SaveData(ad, _dataKey);
    }
}
