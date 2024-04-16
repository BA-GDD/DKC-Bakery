using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityTargettingSystem : MonoBehaviour
{
    private bool _canBinding = true;
    public bool CanBinding
    {
        get => _canBinding;
        set
        {
            _canBinding = value;
            if(_getTargetArrowDic.ContainsKey(_selectCard))
            {
                _getTargetArrowDic[_selectCard][0].ActiveArrow(value);
            }
        }
    }
    [SerializeField] private BattleController _battleController;

    [Header("마우스 카드 바인딩")]
    [SerializeField] private AbilityTargetArrow _targetArrowPrefab;
    private Dictionary<CardBase, List<AbilityTargetArrow>> _getTargetArrowDic = new ();
    private CardBase _selectCard;
    public Vector2 mousePos;
    private bool _isBindingMouseAndCard;

    [Header("적 확인")]
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private Transform _chainImPact;
    [SerializeField] private Color _reactionColor;
    [SerializeField] private ChainSelectTarget _chainTarget;

    public void AllGenerateChainPos(bool isGenerate)
    {
        List<CardBase> onActiveZoneList = CardReader.SkillCardManagement.InCardZoneList;

        foreach (CardBase cb in onActiveZoneList)
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

    public void ActivationCardSelect(CardBase selectCard)
    {
        List<CardBase> onActiveZoneList = CardReader.SkillCardManagement.InCardZoneList;

        foreach (CardBase cb in onActiveZoneList)
        {
            if (_getTargetArrowDic.ContainsKey(cb))
            {
                foreach (AbilityTargetArrow ata in _getTargetArrowDic[cb])
                {
                    if (cb == selectCard)
                    {
                        ata.SetFade(1);
                        continue;
                    }
                    ata.SetFade(0.1f);
                }
            }
        }
    }

    public void ChainFadeControl(float fadeValue)
    {
        List<CardBase> onActiveZoneList = CardReader.SkillCardManagement.InCardZoneList;

        foreach (CardBase cb in onActiveZoneList)
        {
            if (_getTargetArrowDic.ContainsKey(cb))
            {
                foreach (AbilityTargetArrow ata in _getTargetArrowDic[cb])
                {
                    ata.SetFade(fadeValue);
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
            foreach(Enemy e in _battleController.onFieldMonsterList)
            {
                if (e is null) continue;

                _selectCard = selectCard;
                AbilityTargetArrow ata = Instantiate(_targetArrowPrefab, transform);
                ata.ActiveArrow(false);
                if (!_getTargetArrowDic.ContainsKey(selectCard))
                {
                    List<AbilityTargetArrow> atlist = new();
                    _getTargetArrowDic.Add(selectCard, atlist);
                }
                _getTargetArrowDic[selectCard].Add(ata);

                ata.transform.position = selectCard.transform.position;

                Vector2 screenPoint = MaestrOffice.GetScreenPosToWorldPos(e.transform.position);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.CanvasTrm, screenPoint, UIManager.Instance.Canvas.worldCamera, out Vector2 anchoredPosition);

                Debug.Log(anchoredPosition);
                int idx = _getTargetArrowDic[_selectCard].Count - 1;
                _getTargetArrowDic[_selectCard][idx].ArrowBinding(_selectCard.transform, anchoredPosition);
                _getTargetArrowDic[_selectCard][idx].SetFade(0.5f);

                EnemyMarking(e);
            }
        }

        _battleController.Player.VFXManager.BackgroundColor(Color.white);
    }

    private void BindMouseAndCardWithArrow()
    {
        if(CanBinding)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.CanvasTrm,
                                                                Input.mousePosition, Camera.main, out mousePos);
        }

        int idx = _getTargetArrowDic[_selectCard].Count - 1;
        _getTargetArrowDic[_selectCard][idx].ArrowBinding(_selectCard.transform, mousePos);
        _getTargetArrowDic[_selectCard][idx].SetFade(0.5f);
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
                EnemyMarking(e);
                ActivationCardSelect(_selectCard);
            }
        }
    }

    private void EnemyMarking(Enemy e)
    {
        e.SelectedOnAttack(_selectCard);

        int idx = _getTargetArrowDic[_selectCard].Count - 1;
        //_getTargetArrowDic[_selectCard][idx].ArrowBinding(_selectCard.transform, _mousePos);
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
    
    private void Update()
    {
        if(_isBindingMouseAndCard )
        {
            BindMouseAndCardWithArrow();
            CheckSelectEnemy();
        }
    }
}
