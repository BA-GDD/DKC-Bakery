using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultProfilePanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _profileImg;
     
    private int _xTriggerHash = Animator.StringToHash("X_Trigger");

    public void SetProfile(Sprite visual)
    {
        _profileImg.sprite = visual;
    }

    public void DeathMarking()
    {
        _animator.SetTrigger(_xTriggerHash);
    }
}
