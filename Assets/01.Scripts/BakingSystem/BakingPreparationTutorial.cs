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

    [SerializeField]
    private List<ItemDataIngredientSO> _necessaryIngredList; // �ʿ��� ��� ���
    private bool _isCompleted = false;                       // ��� ��Ḧ ��Ҵ���
    private bool _theFirstPickUp = true;                     // ó������ �������� �ֿ�����
    private bool _isFirstOpenInventory = true;               // ó������ �κ��丮�� ��������
    [SerializeField]
    private GameObject _inventoryTutorialPanel;              // �κ��丮 Ʃ�丮�� �г�(�ϳ��� ���� ������ ����)

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

        // �ʿ��� ��� ��ϸ�ŭ �ݺ�
        for (int i = 0; i < _necessaryIngredList.Count; ++i)
        {
            // ���� �κ��丮 ��ųʸ��� �ϳ��� ���ٸ�
            if (!Inventory.Instance.ingredientStash.stashDictionary.ContainsKey(_necessaryIngredList[i]))
            {
                // ���� ���� �ݺ��� Ż��
                _isCompleted = false;
                break;
            }
            // ���� �κ��丮 ��ųʸ��� �ִٸ�
            else
            {
                // �켱 ������ ���
                _isCompleted = true;
            }
        }

        // ���� ���� �� �����ߴٸ�(�ʿ��� ��� ��ᰡ �ִٸ�)
        if (_isCompleted)
        {
            // ���� ���� �� ���� ����ũ �����ڴ� �̾߱� ���
            GameManager.Instance.Player.onPickUpItem -= TraverseInventoryHandle;
        }
    }

    private void Update()
    {
        // �κ��丮 Ʃ�丮���� ������ ���� Ŭ���ϸ� ���������ϱ� ������
        // Ŭ���޴� �ڵ� �ۼ��ؾ���
    }
}
