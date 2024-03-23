using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultProfilePanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _profileImg;

    public void SetProfile(Sprite visual)
    {
        _profileImg.sprite = visual;
        RectTransform vrt = _profileImg.transform as RectTransform;
        RectTransform rt = transform as RectTransform;
        vrt.sizeDelta = new Vector2(rt.sizeDelta.x - 30, rt.sizeDelta.y - 30);
    }

    public void DeathMarking()
    {
        _animator.speed = 1f;
    }
}
