using EpisodeDialogueDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterStand : MonoBehaviour
{
    [SerializeField] private Image _characterStandImg;
    [SerializeField] private List<Sprite> _characterEmoList = new List<Sprite>();

    public EmotionType EmotionType
    {
        set
        {
            _characterStandImg.sprite = _characterEmoList[(int)value];
        }
    }

    private void Start()
    {
        transform.DOLocalMoveY(transform.localPosition.y + 10, 1.6f).SetLoops(-1, LoopType.Yoyo);
    }

    public void JumpAction()
    {
        Vector2 normalValue = transform.localPosition;
        Vector2 jumpValue = normalValue + new Vector2(0, 30);
        transform.DOLocalJump(jumpValue, 10, 1, 0.5f);
    }
}
