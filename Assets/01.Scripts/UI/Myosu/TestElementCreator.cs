using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestElementCreator : MonoBehaviour
{
    [SerializeField] private MyosuTestInfo[] _testArr;
    [SerializeField] private RectTransform _testElementParentRect;

    private void Start()
    {
        _testElementParentRect.sizeDelta = 
        new Vector2(0, Mathf.Clamp((_testArr.Length * 200) + 100, 1000, int.MaxValue));

        for(int i = 0; i < _testArr.Length; i++)
        {
            TestElement te = PoolManager.Instance.Pop(PoolingType.TestElement) as TestElement;
            te.transform.localScale = Vector3.one;
            te.transform.SetParent(_testElementParentRect, false);

            te.TestIdx = i + 1;
            te.TestInfo = _testArr[i];
        }
    }
}
