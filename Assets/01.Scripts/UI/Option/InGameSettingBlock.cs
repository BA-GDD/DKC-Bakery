using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InGameSettingBlock : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private TextMeshProUGUI _notifyIsChangeText;
    [SerializeField] private TMP_InputField _vibrationValueField;
    [SerializeField] private TMP_Dropdown _screenModeDropDown;
    [SerializeField] private CheckBox _verticalSyncCheckBox;
    [SerializeField] private SaveBtn _saveBtn;
    [SerializeField] private SetInitialValueBtn _setInitialBtn;

    private InGameSettingData _inGameSettingData = new InGameSettingData();
    private bool _isHasChanges;

    private Regex _numberFilter = new Regex(@"^[0-9]+$");

    private const string InGameSettingDatakey = "InGameDataKey";

    private void Awake()
    {
        _vibrationValueField.onValueChanged.AddListener(ChangeVibrationValue);
        _screenModeDropDown.onValueChanged.AddListener(ChangeModeType);
    }

    public void Start()
    {
        if(DataManager.Instance.IsHaveData(InGameSettingDatakey))
        {
            _inGameSettingData = DataManager.Instance.LoadData<InGameSettingData>(InGameSettingDatakey);
        }

        _vibrationValueField.text = _inGameSettingData.vibrationValue.ToString();
        _screenModeDropDown.value = _inGameSettingData.modeNum;
        _verticalSyncCheckBox.IsActive = _inGameSettingData.isVerticalSync;
    }

    public void SaveData()
    {
        _saveBtn.SaveData(_inGameSettingData, InGameSettingDatakey, out _isHasChanges);
        _notifyIsChangeText.enabled = _isHasChanges;
    }

    public void SetInitialValue()
    {
        _setInitialBtn.InitializeData(_inGameSettingData, out _isHasChanges);
        _notifyIsChangeText.enabled = _isHasChanges;
    }

    private void ChangeVibrationValue(string sentencec)
    {
        sentencec = sentencec.Trim();
        if(!_numberFilter.IsMatch(sentencec))
        {
            // 숫자 아닌거 섞임
            return;
        }

        int value = Convert.ToInt32(sentencec);

        if(value < 0 || value > 100)
        {
            // 값 벗어남
            return;
        }

        _inGameSettingData.vibrationValue = value;
    }
    private void ChangeModeType(int num)
    {
        _inGameSettingData.modeNum = num;
    }
    public void ClickCheckBox()
    {
        _verticalSyncCheckBox.IsActive = !_verticalSyncCheckBox.IsActive;
    }
}
