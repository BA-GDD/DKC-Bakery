using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffingMark : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CombatMarkingData CombatMarkingData { get; set; }

    [SerializeField] private Image _visual;
    [SerializeField] private RectTransform _infoPanelTrm;
    private Transform _currentInfoPanelTrm;

    [SerializeField] private TextMeshProUGUI _buffNameText;
    [SerializeField] private TextMeshProUGUI _infoText;
    public int VisualIndex { get; private set; }
    private Transform _infoPanelParent;

    private Tween _infoPanelTween;

    public void SetInfo(Sprite visual, string buffName, CombatMarkingData data, Transform panelTrm)
    {
        CombatMarkingData = data;

        _buffNameText.text = buffName;
        _visual.sprite = visual;
        _infoText.text = data.buffingInfo;
        _infoPanelParent = panelTrm;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardReader.AbilityTargetSystem.OnTargetting) return;

        _currentInfoPanelTrm = Instantiate(_infoPanelTrm, _infoPanelParent);
        _infoPanelTween.Kill();
        _infoPanelTween = _currentInfoPanelTrm.DOScaleX(1, 0.1f).SetEase(Ease.OutBounce);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CardReader.AbilityTargetSystem.OnTargetting) return;

        _infoPanelTween.Kill();
        _infoPanelTween = _currentInfoPanelTrm.DOScaleX(0, 0.1f).SetEase(Ease.InBounce);
    }
}
