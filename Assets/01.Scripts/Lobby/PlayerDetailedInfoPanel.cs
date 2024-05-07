using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetailedInfoPanel : PanelUI
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private Tween _scalingTween;

    private AdventureData _adventureData = new AdventureData();
    private const string _adventureKey = "AdventureKEY";

    [Header("플레이어 정보")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nickNameText;

    [Header("진행도")]
    [SerializeField] private TextMeshProUGUI _advenPercent;
    [SerializeField] private TextMeshProUGUI _dungeonPercent;
    [SerializeField] private TextMeshProUGUI _mazePercent;

    [SerializeField] private Image _advenGaze;
    [SerializeField] private Image _dungeonGaze;
    [SerializeField] private Image _mazeGaze;

    private const int _advenMaxStage = 36;
    private const int _dungeonMax = 10;
    private const int _mazeMax = 10;

    [Header("가중치")]
    [SerializeField] private TextMeshProUGUI _atkAddvalueText;
    [SerializeField] private TextMeshProUGUI _defAddvalueText;
    [SerializeField] private TextMeshProUGUI _hpAddvalueText;

    public void EnablePanel()
    {
        _scalingTween?.Kill();

        _canvasGroup.transform.localScale = Vector3.one * 1.1f;
        _canvasGroup.alpha = 1f;
        FadePanel(true);

        _scalingTween = _canvasGroup.transform.DOScale(1, 0.1f);
    }
    public void DisablePanel()
    {
        _scalingTween?.Kill();

        _canvasGroup.transform.localScale = Vector3.one;
        FadePanel(false);

        Sequence seq = DOTween.Sequence();
        _scalingTween = seq;
        seq.Append(_canvasGroup.transform.DOScale(1.1f, 0.1f));
        seq.Join(_canvasGroup.DOFade(0, 0.1f));
    }

    public void SetPlayerData(PlayerData data)
    {
        int level = data.level;

        _levelText.text = $"<size=40><color=#4F2620>LV.</color></size>{level}";
        _nickNameText.text = data.nickName;

        _atkAddvalueText.text = 
        $"<color=#4F2620>ATK: </color>+{GetAddValue(level, 3)}% <color=#4F2620>(합산: {data.attak * GetAddValue(level, 3)})";

        _defAddvalueText.text =
        $"<color=#4F2620>ATK: </color>+{GetAddValue(level, 2)}% <color=#4F2620>(합산: {data.attak * GetAddValue(level, 2)})";

        _hpAddvalueText.text =
        $"<color=#4F2620>ATK: </color>+{GetAddValue(level, 4)}% <color=#4F2620>(합산: {data.attak * GetAddValue(level, 4)})";
    }

    private int GetAddValue(int level, int value)
    {
        return level * 4 * value * Mathf.RoundToInt(Mathf.Pow(level - 1, 3));
    }

    private void Start()
    {
        if (DataManager.Instance.IsHaveData(_adventureKey))
        {
            _adventureData = DataManager.Instance.LoadData<AdventureData>(_adventureKey);
        }

        string[] advenValue = _adventureData.InChallingingStageCount.Split('-');
        int advenCount = (Convert.ToInt16(advenValue[0]) - 1) * 6 + Convert.ToInt16(advenValue[1]);
        int dunCount = Convert.ToInt16(_adventureData.ChallingingMineFloor);
        int mazeCount = Convert.ToInt16(_adventureData.InChallingingMazeLoad);

        advenCount = advenCount / _advenMaxStage;
        dunCount = dunCount / _dungeonMax;
        mazeCount = mazeCount / _mazeMax;

        _advenGaze.fillAmount = advenCount;
        _dungeonGaze.fillAmount = dunCount;
        _mazeGaze.fillAmount = mazeCount;

        _advenPercent.text = $"{advenCount * 100}%";
        _dungeonPercent.text = $"{dunCount * 100}%";
        _mazePercent.text = $"{mazeCount * 100}%";
    }
}
