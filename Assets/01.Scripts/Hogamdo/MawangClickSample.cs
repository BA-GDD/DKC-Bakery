using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MawangClickSample : MonoBehaviour
{
    [Header("외부참조")]
    [SerializeField] private Camera _mainCam;
    [SerializeField] private LayerMask _whatisMawangLayer;

    [Header("이벤트")]
    [SerializeField] private UnityEvent<int> _addLikebilityShameEvent;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray checkMawangRay = _mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log(1);
            if(Physics.Raycast(checkMawangRay, out hit))
            {
                Debug.Log(2);
                AddLikeabilityShame(10); //임시로 10
            }
        }
    }

    private void AddLikeabilityShame(int shame)
    {
        _addLikebilityShameEvent?.Invoke(shame);
    }
}
