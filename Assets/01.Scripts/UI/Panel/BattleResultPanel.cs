using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultPanel : PanelUI
{
    [Header("배틀 리절트 패널")]
    [SerializeField] private BattleController _battleController;
    [SerializeField] private GameObject _clearText;
    [SerializeField] private Transform _enemyProfileTrm;
    [SerializeField] private BattleResultProfilePanel _elementProfile;
    [SerializeField] private Transform _itemProfileTrm;

    public void SetClear()
    {
        _clearText.SetActive(true);
        _clearText.transform.localScale = Vector3.one * 1.5f;
        Sequence seq = DOTween.Sequence();

        seq.Append(_clearText.transform.DOScale(Vector3.one, 0.1f));

        SetEnemyProfile();
    }

    private void SetEnemyProfile()
    {
        List<BattleResultProfilePanel> battleResultEnemyProfiles = new List<BattleResultProfilePanel>();
        Enemy[] stageInEnemies = MapManager.Instanace.SelectStageData.enemyGroup.enemies.ToArray();
        foreach (Enemy e in stageInEnemies)
        {
            BattleResultProfilePanel erp = Instantiate(_elementProfile, _enemyProfileTrm);
            erp.SetProfile(e.CharStat.characterVisual);
            battleResultEnemyProfiles.Add( erp );
        }
        StartCoroutine(KillEnemyMarking(battleResultEnemyProfiles, stageInEnemies));
    }

    private IEnumerator KillEnemyMarking(List<BattleResultProfilePanel> brelist, Enemy[] sieArr)
    {
        for(int i = 0; i < sieArr.Length; i++)
        {
            for (int j = 0; j < _battleController.DeathEnemyList.Count; j++)
            {
                if (sieArr[i] == _battleController.DeathEnemyList[j])
                {
                    brelist[i].DeathMarking();
                }
            }
        }

        yield return new WaitForSeconds(1f);

        if(MapManager.Instanace.SelectStageData.clearCondition.IsClear)
        {
            foreach(ItemDataIngredientSO i in Inventory.Instance.GetIngredientInThisBattle)
            {
                Instantiate(_elementProfile, _itemProfileTrm).SetProfile(i.itemIcon);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
