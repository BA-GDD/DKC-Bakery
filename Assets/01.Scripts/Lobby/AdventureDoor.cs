using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class AdventureDoor : LobbyDoor
{
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;

    private void Start()
    {
        _leftDoor.rotation = Quaternion.identity;
        _rightDoor.rotation = Quaternion.identity;
    }

    protected override void DoorOpen()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_leftDoor.DORotate(new Vector3(0, -90, 0), 0.4f));
        seq.Join(_rightDoor.DORotate(new Vector3(0, 90, 0), 0.4f));
        seq.AppendInterval(0.1f);
        seq.AppendCallback(() => base.DoorOpen());
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && _isInit)
        {
            DoorOpen();
        }
    }
}
