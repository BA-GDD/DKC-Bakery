using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AbilityTargetArrow : MonoBehaviour
{
    [SerializeField] private RectTransform _chainArrow;
    [SerializeField] private RectTransform _arrowTrm;

    public void ArrowBinding(Vector2 startPos, Vector2 endPos)
    {
        _chainArrow.position = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
        transform.localPosition = startPos;

        SetAngle((endPos - startPos).normalized);
        SetLength(startPos, endPos);
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
        _chainArrow.localRotation = rotation;
    }
}
