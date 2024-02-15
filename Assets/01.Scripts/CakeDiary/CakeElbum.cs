using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeElbum : PoolableMono
{
    [SerializeField] private Image _picture;

    public void SetUp(Sprite pictureSprite, float angle)
    {
        _picture.sprite = pictureSprite;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public override void Init()
    {

    }
}
