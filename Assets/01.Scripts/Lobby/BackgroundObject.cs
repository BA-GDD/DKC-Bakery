using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    [SerializeField] private float _slowValue;
    private float _currentY;

    private void Start()
    {
        _currentY = transform.position.y;
    }

    public void MoveObject(float dirValue)
    {
        Vector3 dir = new Vector3(-dirValue * 0.002f, 0);
        transform.position = Vector2.ClampMagnitude(transform.position + dir, 0.5f);
        transform.position = new Vector3(transform.position.x, _currentY);
    }
}
