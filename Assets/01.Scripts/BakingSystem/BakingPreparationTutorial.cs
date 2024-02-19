using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using UnityEngine.InputSystem;

public class BakingPreparationTutorial : MonoBehaviour
{
    /// <summary>
    /// �ʿ��� ����
    /// ���� óġ �� ��ȭ ���� - �κ��丮������ �̷� �� �� �� �ִ� �˷���
    /// (Ʃ�丮���� ���� ��Ḧ ��� ������ �������� �ƹ��͵� ����)
    /// �ʿ��� ��Ḧ ��� ���� �� ��ȭ ���� - ���� ���� �� ������� �� ���ڴ� ����
    /// </summary>

    //�ʿ��� ��� ���
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
            // �κ� ���� ��ư �˷��ִ� ȥ�㸻 ���
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
            // ���� ���� �� ���� ����ũ �����ڴ� �̾߱� ���
            GameManager.Instance.Player.onPickUpItem -= TraverseInventoryHandle;
        }
    }
}
