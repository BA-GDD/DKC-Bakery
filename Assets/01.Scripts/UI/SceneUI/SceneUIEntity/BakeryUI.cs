using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;
using UnityEngine.UI;

public class BakeryUI : SceneUI
{
    [SerializeField] private Transform _popupPanelTrm;
    [SerializeField] private GameObject _bakingCakePanelObj;
    [SerializeField] private Image _cakeImg;

    private NormalPanelInfo _warningPanelInfo;

    private void Start()
    {
        _warningPanelInfo = new NormalPanelInfo
            (
                "경고!",
                "모든 종류의 재료를 넣어 주세요.",
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
