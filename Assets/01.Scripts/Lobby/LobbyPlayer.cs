using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    public float MoveDirY { get; set; }
    [SerializeField] private UnityEvent<float> _playerMoveEvent = null;

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        transform.position += new Vector3(inputX, MoveDirY, 0) * _speed;
        if(inputX != 0)
        {
            _playerMoveEvent?.Invoke(inputX);
        }
    }
}
