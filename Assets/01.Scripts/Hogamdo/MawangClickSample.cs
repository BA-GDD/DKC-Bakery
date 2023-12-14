using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MawangClickSample : MonoBehaviour
{
    [Header("�ܺ�����")]
    [SerializeField] private Camera _mainCam;
    [SerializeField] private LayerMask _whatisMawangLayer;

    [Header("�̺�Ʈ")]
    [SerializeField] private UnityEvent<int> _addLikebilityShameEvent;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AddLikeabilityShame(100); //�ӽ÷� 10
        }
    }

    private void AddLikeabilityShame(int shame)
    {
        _addLikebilityShameEvent?.Invoke(shame);
    }
}
