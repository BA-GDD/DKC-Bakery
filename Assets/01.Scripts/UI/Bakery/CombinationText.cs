using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CombinationText : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _myText;
    [SerializeField] private List<string> _textList = new List<string>();
    private int _idx = 0;

    private Sequence _seq;

    public void OnPointerClick(PointerEventData eventData)
    {
        _idx = (_idx + 1) % _textList.Count;
        _myText.text = _textList[_idx];

        _seq = DOTween.Sequence();
        _seq.Append(transform.DOScale(Vector3.one * 1.1f, 0.15f));
        _seq.Append(transform.DOScale(Vector3.one, 0.15f));
    }
}
