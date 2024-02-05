using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BakeryDoor : LobbyDoor
{
    [SerializeField] private Transform _door;

    private void Start()
    {
        _door.rotation = Quaternion.identity;
    }

    protected override void DoorOpen()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_door.DORotate(new Vector3(0, -90, 0), 0.4f));
        seq.AppendInterval(0.1f);
        seq.AppendCallback(() => base.DoorOpen());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isInit)
        {
            DoorOpen();
        }
    }
}
