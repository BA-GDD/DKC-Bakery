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
            Ray checkMawangRay = _mainCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(checkMawangRay, 100, _whatisMawangLayer))
            {
                AddLikeabilityShame(100); //�ӽ÷� 100
            }
        }
    }

    private void AddLikeabilityShame(int shame)
    {
        _addLikebilityShameEvent?.Invoke(shame);
    }
}
