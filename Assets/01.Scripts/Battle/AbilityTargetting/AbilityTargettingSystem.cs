using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargettingSystem : MonoBehaviour
{
    [SerializeField] private BattleController _battleController;

    [Header("마우스 카드 바인딩")]
    [SerializeField] private AbilityTargetArrow _targetArrowPrefab;
    private Dictionary<CardBase, AbilityTargetArrow> _getTargetArrowDic = new ();
    private CardBase _selectCard;
    private Vector2 _mousePos;
    private bool _isBindingMouseAndCard;

    [Header("적 확인")]
    [SerializeField] private LayerMask _whatIsEnemy;

    public void AllGenerateChainPos(List<CardBase> onActiveZoneList, bool isGenerate)
    {
        foreach(CardBase cb in onActiveZoneList)
        {
            if(_getTargetArrowDic.ContainsKey(cb))
            {
                _getTargetArrowDic[cb].IsGenerating = isGenerate;
            }
        }
    }

    public void SetMouseAndCardArrowBind(CardBase selectCard)
    {
        selectCard.CanUseThisCard = false;
        _battleController.Player.VFXManager.BackgroundColor(Color.gray);

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

        _getTargetArrowDic[_selectCard].ArrowBinding(_selectCard.transform, _mousePos);
    }

    private void CheckSelectEnemy()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0, _whatIsEnemy);

            if (hit.transform == null) return;

            if (hit.transform.TryGetComponent<Enemy>(out Enemy e))
            {
                e.ActiveOnAttackMarking();
                _battleController.Player.VFXManager.BackgroundColor(Color.white);

                _getTargetArrowDic[_selectCard].ArrowBinding(_selectCard.transform, _mousePos);
                _getTargetArrowDic[_selectCard].SetFade();
                _isBindingMouseAndCard = false;
            }
        }
    }
    
    private void Update()
    {
        if(_isBindingMouseAndCard )
        {
            BindMouseAndCardWithArrow();
            CheckSelectEnemy();
        }
    }
}
