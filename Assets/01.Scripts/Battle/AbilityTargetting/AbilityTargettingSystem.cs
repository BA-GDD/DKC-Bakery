using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargettingSystem : MonoBehaviour
{
    private PlayerVFXManager _playerVFXManager;

    [Header("마우스 카드 바인딩")]
    [SerializeField] private AbilityTargetArrow _targetArrowPrefab;
    private Dictionary<CardBase, AbilityTargetArrow> _getTargetArrowDic = new Dictionary<CardBase, AbilityTargetArrow>();
    private CardBase _selectCard;
    private Vector2 _mousePos;
    private bool _isBindingMouseAndCard;

    private void Awake()
    {
        _playerVFXManager = FindObjectOfType<PlayerVFXManager>();
    }

    public void SetMouseAndCardArrowBind(CardBase selectCard)
    {
        selectCard.CanUseThisCard = false;
        _playerVFXManager.BackgroundColor(Color.gray);

        _selectCard = selectCard;
        AbilityTargetArrow ata = Instantiate(_targetArrowPrefab, transform);
        _getTargetArrowDic.Add(selectCard, ata);

        ata.transform.position = selectCard.transform.position;
        _isBindingMouseAndCard = true;
    }

    private void BindMouseAndCardWithArrow()
    {
        if (_mousePos == MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition)) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.CanvasTrm, 
                                                                Input.mousePosition, Camera.main, out _mousePos);

        _getTargetArrowDic[_selectCard].ArrowBinding(_selectCard.transform.localPosition, _mousePos);
    }
    
    private void Update()
    {
        if(_isBindingMouseAndCard )
        {
            BindMouseAndCardWithArrow();
        }
    }
}
