using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stageNameText;
    [SerializeField] private Transform _infoBlockTrm;

    public void SetInfo(string stageName /*스테이지 정보*/, bool isReverse)
    {
        _stageNameText.text = stageName;

        if(isReverse)
        {
            transform.rotation = Quaternion.Euler(0, 0, -180);
            _infoBlockTrm.localRotation = Quaternion.Euler(0, 0, -180);
        }
    }
}
