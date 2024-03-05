using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UIDefine;

public class InventoryTrigger : MonoBehaviour
{
    [SerializeField] private Transform _invenParent;
    private PanelUI _invenPanel;
    private bool _isOnInventory;

    private void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            if(!_isOnInventory)
            {
                _invenPanel = PanelManager.Instance.CreatePanel(PanelType.inventory, _invenParent, Vector2.zero);
                _invenPanel.ActivePanel(true);
            }
            else
            {
                _invenPanel.ActivePanel(false);
            }
            _isOnInventory = !_isOnInventory;
        }
    }
}
