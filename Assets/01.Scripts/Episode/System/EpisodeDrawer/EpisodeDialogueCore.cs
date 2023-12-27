using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EpisodeDialogueDefine;
using System;

public class EpisodeDialogueCore : MonoBehaviour
{
    [SerializeField] private EpisodeData _selectEpisodeData;
    private DialogueElement _selectDialogueElement;

    [SerializeField] private UnityEvent<string, string, BackGroundType> StandardDrawEvent;
    [SerializeField] private UnityEvent<FadeOutType> ProductionDrawEvent;
    [SerializeField] private UnityEvent<CharacterType, FaceType, bool, bool> CharacterDrawEvent;
    [SerializeField] private UnityEvent<CharacterType, MoveType, ExitType> CharacterMoveEvent;
    [SerializeField] private UnityEvent<CharacterType, EmotionType> CharacterEmotionEvent;

    public void HandleEpisodeStart(EpisodeData episodeData)
    {
        _selectEpisodeData = episodeData;
    }

    public void HandleNextDialogue()
    {
        _selectDialogueElement = _selectEpisodeData.dialogueElement[EpisodeManager.Instanace.dialogueIdx];
        PhaseEventConnect();
        Debug.Log(_selectEpisodeData.dialogueElement[EpisodeManager.Instanace.dialogueIdx].isLinker);
        while (_selectEpisodeData.dialogueElement[EpisodeManager.Instanace.dialogueIdx].isLinker)
        {
            _selectDialogueElement = _selectEpisodeData.dialogueElement[EpisodeManager.Instanace.dialogueIdx];
            DialogueElement beforeDe = _selectEpisodeData.dialogueElement[EpisodeManager.Instanace.dialogueIdx - 1];
            PhaseLinkConnect(beforeDe.standardElement.name, beforeDe.standardElement.sentence);
        }
    }

    private void PhaseLinkConnect(string name, string syntex)
    {
        StandardDrawEvent?.Invoke(name,
                                  syntex,
                                  _selectDialogueElement.standardElement.backGroundType);

        PhaseConnectStandard();
    }

    private void PhaseEventConnect()
    {
        
        StandardDrawEvent?.Invoke(_selectDialogueElement.standardElement.name,
                                  _selectDialogueElement.standardElement.sentence,
                                  _selectDialogueElement.standardElement.backGroundType);

        PhaseConnectStandard();
    }

    private void PhaseConnectStandard()
    {
        ProductionDrawEvent?.Invoke(_selectDialogueElement.productElement.fadeType);

        CharacterDrawEvent?.Invoke(_selectDialogueElement.characterElement.characterType,
                                   _selectDialogueElement.characterElement.faceType,
                                   _selectDialogueElement.characterElement.isActive,
                                   _selectDialogueElement.characterElement.isShake);

        CharacterMoveEvent?.Invoke(_selectDialogueElement.characterElement.characterType,
                                   _selectDialogueElement.movementElement.moveType,
                                   _selectDialogueElement.movementElement.exitTpe);

        CharacterEmotionEvent?.Invoke(_selectDialogueElement.characterElement.characterType,
                                      _selectDialogueElement.characterElement.emotionType);

        EpisodeManager.Instanace.AddDialogeLogData(_selectDialogueElement.characterElement.characterType,
                                                   _selectDialogueElement.standardElement.name,
                                                   _selectDialogueElement.standardElement.sentence);
        EpisodeManager.Instanace.dialogueIdx++;
    }
}
