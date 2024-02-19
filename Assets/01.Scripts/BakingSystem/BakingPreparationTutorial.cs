using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.InputSystem;

public class BakingPreparationTutorial : MonoBehaviour
{
    /// <summary>
    /// 필요한 내용
    /// 몬스터 처치 후 대화 진행 - 인벤토리에서는 이런 걸 할 수 있다 알려줌
    /// (튜토리얼을 위한 재료를 모두 모으기 전까지는 아무것도 없음)
    /// 필요한 재료를 모두 모은 후 대화 진행 - 대충 이제 다 모았으니 집 가자는 내용
    /// </summary>

    //필요한 재료 목록
    [SerializeField]
    private List<ItemDataIngredientSO> _necessaryIngredList;
    private bool _isCompleted = false;
    private bool _theFirstPickUp = true;
    [SerializeField]
    private GameObject _inventoryTutorialPanel;

    private void Start()
    {
        GameManager.Instance.Player.onPickUpItem += TraverseInventoryHandle;
        
    }

    public void ShowInventoryTutorialHandle()
    {
        _inventoryTutorialPanel.SetActive(true);
    }

    public void TraverseInventoryHandle()
    {
        if (_theFirstPickUp)
        {
            // 인벤 여는 버튼 알려주는 혼잣말 출력
            _theFirstPickUp = false;
        }

        for (int i = 0; i < _necessaryIngredList.Count; ++i)
        {
            if (!Inventory.Instance.ingredientStash.stashDictionary.ContainsKey(_necessaryIngredList[i]))
            {
                _isCompleted = false;
                break;
            }
            else
            {
                _isCompleted = true;
            }
        }

        if (_isCompleted)
        {
            // 대충 이제 집 가서 케이크 만들자는 이야기 출력
            GameManager.Instance.Player.onPickUpItem -= TraverseInventoryHandle;
        }
    }
}
