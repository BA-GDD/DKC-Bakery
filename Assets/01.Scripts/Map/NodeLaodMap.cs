using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLaodMap : MonoBehaviour
{
    [SerializeField] private MapNode[] _nodeArr;
    [SerializeField] private RectTransform _linePrefab;
    [SerializeField] private Transform _lineTrm;
    [SerializeField] private Transform _deckSelectParent;

    [SerializeField] private Transform _bubbleTrm;
    public Transform BubbleTrm => _bubbleTrm;

    private void Start()
    {
        for(int i = 0; i < _nodeArr.Length - 1; i++)
        {
            RectTransform line = Instantiate(_linePrefab, _lineTrm);
            line.name = $"NodeLine_{i}";

            Vector2 firstNodePos = _nodeArr[i].transform.localPosition;
            Vector2 secondNodePos = _nodeArr[i + 1].transform.localPosition;

            Vector2 centerPos = (firstNodePos + secondNodePos) / 2;

            Vector2 dir = (secondNodePos - firstNodePos).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            float length = Mathf.Sqrt(Mathf.Pow(secondNodePos.x - firstNodePos.x, 2)
                                    + Mathf.Pow(secondNodePos.y - firstNodePos.y, 2));

            line.transform.localPosition = centerPos;
            line.transform.rotation = Quaternion.Euler(0, 0, angle);
            line.sizeDelta = new Vector2(length, 20);

            _nodeArr[i].StageNumber = i;
        }
    }
    public void ExitLoadMap()
    {
        MapManager.Instanace.ActiveLoadMapPanel(false);
    }
}
