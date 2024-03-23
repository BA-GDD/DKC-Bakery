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
    [SerializeField]
    private GameObject _inventoryTutorialPanel;              // �κ��丮 Ʃ�丮�� �г�(�ϳ��� ���� ������ ����)

    private void Start()
    {
        //GameManager.Instance.Player.onPickUpItem += TraverseInventoryHandle;
    }

    public void TraverseInventoryHandle()
    {
        if (DataManager.Instance.CheckOnFirstData.isFirstPickUpitem)
        {
            // �κ� ���� ��ư �˷��ִ� ȥ�㸻 ���
            Debug.Log("�κ� �����");
            DataManager.Instance.CheckOnFirstData.isFirstPickUpitem = false;
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
            Debug.Log("�� ���� ����ũ ������");
            //GameManager.Instance.Player.onPickUpItem -= TraverseInventoryHandle;
        }
    }

    //private void Update()
    //{
    //    if (Keyboard.current.yKey.wasPressedThisFrame)
    //    {
    //        Inventory.Instance.AddItem(_necessaryIngredList[0]);
    //        TraverseInventoryHandle();
    //    }
    //    if (Keyboard.current.uKey.wasPressedThisFrame)
    //    {
    //        Inventory.Instance.AddItem(_necessaryIngredList[1]);
    //        TraverseInventoryHandle();
    //    }
    //    if (Keyboard.current.iKey.wasPressedThisFrame)
    //    {
    //        Inventory.Instance.AddItem(_necessaryIngredList[2]);
    //        TraverseInventoryHandle();
    //    }
    //    if (Keyboard.current.oKey.wasPressedThisFrame)
    //    {
    //        Inventory.Instance.AddItem(_necessaryIngredList[3]);
    //        TraverseInventoryHandle();
    //    }
    //    if (Keyboard.current.pKey.wasPressedThisFrame)
    //    {
    //        Inventory.Instance.AddItem(_necessaryIngredList[4]);
    //        TraverseInventoryHandle();
    //    }

    //    if (Keyboard.current.aKey.wasPressedThisFrame
    //        && _isFirstOpenInventory)
    //    {
    //        ShowInventoryTutorialHandle();
    //        _isFirstOpenInventory = false;
    //    }
    //}
}
