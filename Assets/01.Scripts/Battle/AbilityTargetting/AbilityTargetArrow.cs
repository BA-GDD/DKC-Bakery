using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTargetArrow : MonoBehaviour
{
    [SerializeField] private Image _chainArrowVisual;
    [SerializeField] private Image[] _chainVisual;
    [SerializeField] private RectTransform _arrowTrm;

    private Transform _saveStartTrm;
    private Vector2 _saveEndPos;
    
    private Sequence _fadeSequence;
    public bool IsGenerating { get; set; }

    public void SetFade()
    {
        _fadeSequence.Kill();

        _fadeSequence = DOTween.Sequence();

        _fadeSequence.Append(_chainArrowVisual.DOFade(0f, 0.2f));
        foreach(var chain in _chainVisual)
        {
            _fadeSequence.Join(chain.DOFade(0.5f, 0.2f));
        }
    }

    public void SetActive()
    {
        _fadeSequence.Kill();

        _fadeSequence = DOTween.Sequence();

        foreach (var chain in _chainVisual)
        {
            _fadeSequence.Join(chain.DOFade(1f, 0.2f));
        }
    }

    private void Update()
    {
        if(IsGenerating)
        {
            ArrowBinding(_saveStartTrm, _saveEndPos);
        }
    }

    public void ArrowBinding(Transform startTrm, Vector2 endPos)
    {
        _chainArrowVisual.transform.position = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
        transform.localPosition = startTrm.localPosition;

        SetAngle((endPos - (Vector2)startTrm.localPosition).normalized);
        SetLength(startTrm.localPosition, endPos);

        _saveEndPos = endPos;
        _saveStartTrm = startTrm;
    }

    private void SetLength(Vector2 startPos, Vector2 endPos)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(endPos.x - startPos.x, 2) + Mathf.Pow(endPos.y - startPos.y, 2));
        _arrowTrm.sizeDelta = new Vector2(distance, _arrowTrm.sizeDelta.y);
    }

    private void SetAngle(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle -180, Vector3.forward);
        _arrowTrm.localRotation = rotation;
        _chainArrowVisual.transform.localRotation = rotation;
    }
}
