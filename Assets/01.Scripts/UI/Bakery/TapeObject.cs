using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeObject : MonoBehaviour
{
    [SerializeField] private float _limitX;
    [SerializeField] private Transform[] _tapeTextArr;

    private void Update()
    {
        for(int i = 0; i < _tapeTextArr.Length; i++)
        {
            _tapeTextArr[i].localPosition += new Vector3(-1, 0, 0);

            if(_tapeTextArr[i].localPosition.x <= _limitX)
            {
                _tapeTextArr[i].localPosition = new Vector3(-_limitX, 0, 0);
            }
        }
    }
}
