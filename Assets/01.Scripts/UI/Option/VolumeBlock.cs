using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VolumeBlock : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private TextMeshProUGUI _notifyIsChangeText;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private SaveBtn _savebtn;
    [SerializeField] private SetInitialValueBtn _setInitialValueBtn;

    private SoundData _soundData = new SoundData();
    private bool _isHasChanges;

    private const string VolumeSettingKey = "VolumeDataKey";

    private void Awake()
    {
        _masterSlider.onValueChanged.AddListener(SetMasterVolumeValue);
        _bgmSlider.onValueChanged.AddListener(SetBgmVolumeValue);
        _sfxSlider.onValueChanged.AddListener(SetSfxVolumeValue);
    }

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(VolumeSettingKey))
        {
            _soundData = DataManager.Instance.LoadData<SoundData>(VolumeSettingKey);
        }

        _masterSlider.value = _soundData.MasterVoume;
        _bgmSlider.value = _soundData.BgmVolume;
        _sfxSlider.value = _soundData.SfxVolume;
    }

    #region 밸류 셋 메서드 개노답 삼인방
    private void SetMasterVolumeValue(float value)
    {
        _soundData.MasterVoume = value;
    }

    private void SetBgmVolumeValue(float value)
    {
        _soundData.BgmVolume = value;
    }

    private void SetSfxVolumeValue(float value)
    {
        _soundData.SfxVolume = value;
    }
    #endregion

    public void SaveData()
    {
        _savebtn.SaveData(_soundData, VolumeSettingKey, out _isHasChanges);
        _notifyIsChangeText.enabled = _isHasChanges;
    }

    public void SetInitialValue()
    {
        _setInitialValueBtn.InitializeData(_soundData, out _isHasChanges);
        _notifyIsChangeText.enabled = _isHasChanges;
    }
}
