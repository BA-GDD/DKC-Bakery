using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AdventureType
{
    Mission,
    Mine,
    Stage
}

public class AdventurePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AdventureType _adventureType;
    private AdventurePanel[] _otherPanels;
    [SerializeField] private float _unSelectedValue;
    private Tween _moveTween;

    private void OnDisable()
    {
        _moveTween?.Kill();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var panel in _otherPanels)
        {
            if (panel == this)
            {
                _moveTween = transform.DOScale(Vector2.one * 1.1f, 0.2f);
                continue;
            }
            panel.PanelUnSelected();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _moveTween?.Kill();

        foreach (var panel in _otherPanels)
        {
            if (panel == this)
            {
                _moveTween = transform.DOScale(Vector2.one, 0.2f);
                continue;
            }
            panel.PanelNormaling();
        }
    }

    public void PanelUnSelected()
    {
        _moveTween?.Kill();
        _moveTween = transform.DOLocalMoveY(_unSelectedValue, 0.2f).SetEase(Ease.InBack);
    }

    public void PanelNormaling()
    {
        _moveTween?.Kill();
        _moveTween = transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.InBack);
    }

    private void Awake()
    {
        _otherPanels = FindObjectsOfType<AdventurePanel>();
    }

    public void GoAdventure()
    {
        switch (_adventureType)
        {
            case AdventureType.Mission:
                break;
            case AdventureType.Mine:
                GameManager.Instance.ChangeScene(SceneList.MineScene);
                break;
            case AdventureType.Stage:
                GameManager.Instance.ChangeScene(SceneList.MapScene);
                break;
        }
    }
}
