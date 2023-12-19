using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EpisodeDialogueDefine;
using System;

public class EpisodeDialogueCore : MonoBehaviour
{
    private int _dialogueIdx = 0;
    private EpisodeData _selectEpisodeData;
    private DialogueElement _selectDialogueElement;

    [SerializeField] private UnityEvent<string, string, BackGroundType> StandardDrawEvent;
    [SerializeField] private UnityEvent<FadeOutType> ProductionDrawEvent;
    [SerializeField] private UnityEvent<CharacterType, FaceType, bool, bool> CharacterDrawEvent;
    [SerializeField] private UnityEvent<CharacterType, MoveType, ExitType> CharacterMoveEvent;

    private void Awake()
    {
        _dialogueIdx = 0;
    }

    public void HandleEpisodeStart(EpisodeData episodeData)
    {
        _selectEpisodeData = episodeData;
    }

    public void HandleNextDialogue()
    {
        _selectDialogueElement = _selectEpisodeData.dialogueElement[_dialogueIdx];

        StandardDrawEvent?.Invoke(_selectDialogueElement.standardElement.name,
                                  _selectDialogueElement.standardElement.sentence,
                                  _selectDialogueElement.standardElement.backGroundType);

        ProductionDrawEvent?.Invoke(_selectDialogueElement.productElement.fadeType);

        CharacterDrawEvent?.Invoke(_selectDialogueElement.characterElement.characterType,
                                   _selectDialogueElement.characterElement.faceType,
                                   _selectDialogueElement.characterElement.isActive,
                                   _selectDialogueElement.characterElement.isShake);

        CharacterMoveEvent?.Invoke(_selectDialogueElement.characterElement.characterType,
                                   _selectDialogueElement.movementElement.moveType,
                                   _selectDialogueElement.movementElement.exitTpe);

        _dialogueIdx++;
    }
}
