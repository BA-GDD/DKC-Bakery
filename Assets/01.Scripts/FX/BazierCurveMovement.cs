using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazierCurveMovement : MonoBehaviour
{
    [SerializeField] private Transform[] trms;//3개
    [SerializeField] private float speed = 1.0f;

    float percent = 0.0f;
    
    private void Update()
    {
        if (trms[2] != null && percent <= 1.0f)
        {
            float dt = Time.deltaTime * speed;

            percent += dt;
            //첫번째 러프
            Vector2 p1 = Vector2.Lerp(trms[0].position, trms[1].position, percent);

            //두번째 러프
            Vector2 p2 = Vector2.Lerp(trms[1].position, trms[2].position, percent); 

            //결과물
            Vector2 result = Vector2.Lerp(p1, p2, percent);

            transform.position = result;
        }
        else
        {
            //gameObject.SetActive(false);
            //transform.position = Vector2.zero;
        }
    }
}
