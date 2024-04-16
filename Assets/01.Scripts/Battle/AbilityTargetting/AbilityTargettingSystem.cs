using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargettingSystem : MonoBehaviour
{
    [SerializeField] private BattleController _battleController;

    [Header("마우스 카드 바인딩")]
    [SerializeField] private AbilityTargetArrow _targetArrowPrefab;
    private Dictionary<CardBase, List<AbilityTargetArrow>> _getTargetArrowDic = new ();
    private CardBase _selectCard;
    private Vector2 _mousePos;
    private bool _isBindingMouseAndCard;

    [Header("적 확인")]
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private Transform _chainImPact;
    [SerializeField] private Color _reactionColor;
    [SerializeField] private ChainSelectTarget _chainTarget;

    public void AllGenerateChainPos(List<CardBase> onActiveZoneList, bool isGenerate)
    {
        foreach(CardBase cb in onActiveZoneList)
        {
            if(_getTargetArrowDic.ContainsKey(cb))
            {
                foreach(AbilityTargetArrow ata in _getTargetArrowDic[cb])
                {
                    ata.IsGenerating = isGenerate;
                }
            }
        }
    }

    public void SetMouseAndCardArrowBind(CardBase selectCard)
    {
        selectCard.CanUseThisCard = false;
        _battleController.Player.VFXManager.BackgroundColor(Color.gray);

        StartCoroutine(EnemyTargetting(selectCard));
    }

    IEnumerator EnemyTargetting(CardBase selectCard)
    {
        TargetEnemyCount tec = selectCard.CardInfo.targetEnemyCount;
        if (tec != TargetEnemyCount.ALL)
        {
            for (int i = 0; i < (int)tec; i++)
            {
                _selectCard = selectCard;
                AbilityTargetArrow ata = Instantiate(_targetArrowPrefab, transform);
                if(!_getTargetArrowDic.ContainsKey(selectCard))
                {
                    List<AbilityTargetArrow> atlist = new();
                    _getTargetArrowDic.Add(selectCard, atlist);
                }
                _getTargetArrowDic[selectCard].Add(ata);

                ata.transform.position = selectCard.transform.position;
                _isBindingMouseAndCard = true;

                yield return new WaitUntil(() => ata.IsBindSucess);
            }
        }
        else
        {

        }

        _battleController.Player.VFXManager.BackgroundColor(Color.white);
    }

    private void BindMouseAndCardWithArrow()
    {
        if (_mousePos == MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition)) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.CanvasTrm, 
                                                                Input.mousePosition, Camera.main, out _mousePos);

        int idx = _getTargetArrowDic[_selectCard].Count - 1;
        _getTargetArrowDic[_selectCard][idx].ArrowBinding(_selectCard.transform, _mousePos);
        _getTargetArrowDic[_selectCard][idx].SetFade();
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
                e.SelectedOnAttack(_selectCard);

                int idx = _getTargetArrowDic[_selectCard].Count - 1;
                _getTargetArrowDic[_selectCard][idx].ArrowBinding(_selectCard.transform, _mousePos);
                Tween t = _getTargetArrowDic[_selectCard][idx].ReChainning(() =>
                {
                    Instantiate(_chainImPact, e.transform.position, Quaternion.identity);
                    DamageTextManager.Instance.PopupReactionText(e.transform.position + new Vector3(0, 1, 0), _reactionColor, "Connect!");

                    ChainSelectTarget cst = Instantiate(_chainTarget, transform);
                    Vector2 screenPoint = MaestrOffice.GetScreenPosToWorldPos(e.transform.position);
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.CanvasTrm, screenPoint, UIManager.Instance.Canvas.worldCamera, out Vector2 anchoredPosition);
                    RectTransform trm = cst.transform as RectTransform;
                    trm.anchoredPosition = anchoredPosition;
                    cst.SetMark();
                });
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
