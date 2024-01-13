using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOnPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private readonly int _playerMoveHash = Animator.StringToHash("isMove");

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * _speed;
        float moveY = Input.GetAxis("Vertical") * _speed;

        transform.position += new Vector3(moveX, moveY);

        _animator.SetBool(_playerMoveHash, moveX != 0 || moveY != 0);
    }
}
