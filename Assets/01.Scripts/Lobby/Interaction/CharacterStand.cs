using EpisodeDialogueDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

[System.Serializable]
public struct InteractionElement
{
    public Sprite characterFaceVisual;
    public string line;
}

public class CharacterStand : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CharacterType _characterType;
    public CharacterType CharacterType => _characterType;

    [SerializeField] private float _movingTurm;
    [SerializeField] private Image _characterStandImg;
    [SerializeField] private List<InteractionElement> _characterInteractionList = new ();
    [SerializeField] private SpeachBubble _speachBubble;

    public Tween idleTween;
    public LobbyInteraction InterAction { get; set; }

    private void Start()
    {
        idleTween = transform.DOLocalMoveY(transform.localPosition.y + 10, _movingTurm).SetLoops(-1, LoopType.Yoyo);
    }

    public void JumpAction()
    {
        Vector2 normalValue = transform.localPosition;
        Vector2 jumpValue = normalValue + new Vector2(0, 30);
        transform.DOLocalJump(jumpValue, 10, 1, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InteractionElement ie = _characterInteractionList[Random.Range(0, _characterInteractionList.Count)];
        _characterStandImg.sprite = ie.characterFaceVisual;
        _speachBubble.SetLine(ie.line);
        JumpAction();
    }
}
