using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LoadingElement
{
    public string loadingName;
    public string characterLine;
    public Sprite characterImg;
}

public class LoadingUI : SceneUI
{
    [Header("셋팅")]
    [SerializeField] private LoadingElement[] _loadingElementArr;

    [Header("참조")]
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TextMeshProUGUI _loadingValue;

    [SerializeField] private TextMeshProUGUI _loadingNameTxt;
    [SerializeField] private TextMeshProUGUI _characterLineTxt;
    [SerializeField] private Image _characterImg;
    [SerializeField] private Image _speachBubble;

    public override void SceneUIStart()
    {
        //LoadingElement selectLoadingElement = 
        //               _loadingElementArr[Random.Range(0, _loadingElementArr.Length)];

        //_loadingNameTxt.text = selectLoadingElement.loadingName;
        //_characterLineTxt.text = selectLoadingElement.characterLine;
        //_characterImg.sprite = selectLoadingElement.characterImg;

        GameManager.Instance.LoadingProgressChanged += HandleUpdateLoadingBar;
    }

    private void OnDisable()
    {
        GameManager.Instance.LoadingProgressChanged -= HandleUpdateLoadingBar;
    }

    private void HandleUpdateLoadingBar(int progress)
    {
        _loadingBar.value = progress;
        _loadingValue.text = $"{progress}%";
    }
}
