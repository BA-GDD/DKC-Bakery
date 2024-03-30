using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MazkeMark : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MyosuTestInfo _mazeInfo;
    private float _minRisePeriod = 0.5f;
    private float _maxRisePeriod = 1.5f;

    private bool _isSelectedThisStage;

    public void OnPointerClick(PointerEventData eventData)
    {
        _isSelectedThisStage = !_isSelectedThisStage;

        UIManager.Instance.GetSceneUI<MyosuUI>().
                           SetUpPanel(_isSelectedThisStage, _mazeInfo);
    }

    private void Start()
    {
        float randomValue = Random.Range(_minRisePeriod, _maxRisePeriod);
        transform.DOLocalMoveY(transform.localPosition.y + 15, randomValue).
                  SetLoops(-1, LoopType.Yoyo);
    }
}
