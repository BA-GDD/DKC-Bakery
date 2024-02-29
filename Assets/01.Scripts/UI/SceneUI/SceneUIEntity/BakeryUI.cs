using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;
using UnityEngine.UI;
using TMPro;

public class BakeryUI : SceneUI
{
    [SerializeField] private Transform _popupPanelTrm;
    [SerializeField] private GameObject _bakingCakePanelObj;

    [Header("����Ʈ ����ũ")]
    [SerializeField] private Image _cakeImg;
    [SerializeField] private TextMeshProUGUI _cakeNameText;
    [SerializeField] private TextMeshProUGUI _cakeInfoText;

    private NormalPanelInfo _warningPanelInfo;

    private void Start()
    {
        _warningPanelInfo = new NormalPanelInfo
            (
                "���!",
                "��� ������ ��Ḧ �־� �ּ���.",
                true
            );
    }

    public void ClearBakingCakePanel(bool value)
    {
        _bakingCakePanelObj.SetActive(value);
        BakingManager.Instance.CookingBox.ClearImgAllSelectedIngredient();
    }

    public void BakeBread()
    {
        if(BakingManager.Instance.CanBake())
        {
            ItemDataBreadSO bakingCakeSO = BakingManager.Instance.BakeBread();
            ClearBakingCakePanel(true);

            _cakeImg.sprite = bakingCakeSO.itemIcon;
            _cakeNameText.text = bakingCakeSO.itemName;
            _cakeInfoText.text = bakingCakeSO.itemInfo;
        }
        else
        {
            NormalPanel bakingWarningPanel = PanelManager.Instance.CreatePanel(PanelType.normal,
                                                                               _popupPanelTrm,
                                                                               Vector3.zero) 
                as NormalPanel;

            bakingWarningPanel.SetUpPanel(_warningPanelInfo);
            bakingWarningPanel.gameObject.SetActive(true);
        }
    }
}
