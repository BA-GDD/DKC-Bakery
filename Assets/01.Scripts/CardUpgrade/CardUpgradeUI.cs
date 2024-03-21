using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class CardUpgradeUI : MonoBehaviour
{
    [SerializeField] private CardUpgrade _cardUpgrade;
    private bool _isOpendInventory = true;

    private CanUseCardData _canUseCardData = new CanUseCardData();
    private const string _canUseCardDataKey = "CanUseCardsDataKey";

    private CardUpgradeCard _cardUpgradeCard;
    public CardUpgradeCard CardUpgradeCard
    {
        get => _cardUpgradeCard;
        set => _cardUpgradeCard = value;
    }

    [Header("트랜스폼 정보")]
    [SerializeField] private RectTransform _canvasTrm;
    [SerializeField] private RectTransform _inventoryTrm;
    [SerializeField] private RectTransform _contentTrm;

    public RectTransform CanvasTrm => _canvasTrm;
    public RectTransform ContentTrm => _contentTrm;

    [Header("게임 오브젝트 정보")]
    [SerializeField] private CardUpgradeCard _upgradeCardPrefab;
    [SerializeField] private CardBase _sampleCard;
    //[SerializeField] private GameObject _cardSelectPrefab

    private void Start()
    {
        GetInventory();
    }

    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            GetInventory();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _cardUpgrade.DowngradeCard(_cardUpgradeCard.CardBase.CardInfo);
        }
    }

    private void GetInventory()
    {
        if (DataManager.Instance.IsHaveData(_canUseCardDataKey))
        {
            _canUseCardData = DataManager.Instance.LoadData<CanUseCardData>(_canUseCardDataKey);
        }

        for (int i = 0; i < _canUseCardData.CanUseCardsList.Count; i++)
        {
            CardUpgradeCard cuc = Instantiate(_upgradeCardPrefab, _contentTrm);
            cuc.SetInfo(_canUseCardData.CanUseCardsList[i]);
        }
    } 

    public void OpenInventoryBtnEvent()
    {
        _isOpendInventory = !_isOpendInventory;

        float posX = _isOpendInventory ? 200 : 0;

        _inventoryTrm.GetComponent<RectTransform>().DOAnchorPosX(posX, 0.1f);
    }

    public void UpgradeCard()
    {
        if (_cardUpgradeCard == null) return;
        print(_cardUpgradeCard.CardBase.CardInfo.CardName);
        if (_cardUpgrade.IsAbleToUpgrade(_cardUpgradeCard.CardBase.CardInfo))
        {
            print("강화 성공");
            _cardUpgrade.UpgradeCard(_cardUpgradeCard.CardBase.CardInfo);
        }
        else
        {
            print("강화 조건 미달성");
        }
    }
}
