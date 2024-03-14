using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UIDefine;
using UnityEngine.EventSystems;

public class LobbyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIScreenType _toGoScreen;
    [SerializeField] private Transform _visualTrm;
    private Tween _hoverTween;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverTween.Kill();
        _hoverTween = _visualTrm.DOScale(Vector2.one * 1.2f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverTween.Kill();
        _hoverTween = _visualTrm.DOScale(Vector2.one, 0.1f);
    }

    private void OnDisable()
    {
        _hoverTween.Kill();
    }

    public void PressThisButton()
    {
        switch (_toGoScreen)
        {
            case UIScreenType.bakery:
                GameManager.Instance.ChangeScene(SceneList.BakeryScene);
                break;
            case UIScreenType.teaTime:
                GameManager.Instance.ChangeScene(SceneList.TeaTimeScene);
                break;
            case UIScreenType.deckBuild:
                GameManager.Instance.ChangeScene(SceneList.DeckBuildScene);
                break;
        }
    }
}
                                             