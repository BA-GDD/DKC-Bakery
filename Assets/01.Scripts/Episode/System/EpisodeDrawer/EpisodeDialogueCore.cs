using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EpisodeDialogueDefine;

public class EpisodeDialogueCore : MonoBehaviour
{
    [SerializeField] private List<EpisodeData> _selectEpisodeDataList;
    private DialogueElement _selectDialogueElement;

    [SerializeField] private UnityEvent<string, string, BackGroundType> StandardDrawEvent;
    [SerializeField] private UnityEvent<FadeOutType> ProductionDrawEvent;
    [SerializeField] private UnityEvent<CharacterType, FaceType, bool, bool> CharacterDrawEvent;
    [SerializeField] private UnityEvent<CharacterType, MoveType, ExitType> CharacterMoveEvent;
    [SerializeField] private UnityEvent<CharacterType, EmotionType> CharacterEmotionEvent;

    public void HandleEpisodeStart(List<EpisodeData> episodeDataList)
    {
        _selectEpisodeDataList = episodeDataList;
        HandleNextDialogue();
    }

    public void HandleNextDialogue()
    {
        if(_selectEpisodeDataList[EpisodeManager.Instanace.EpisodeIdx].dialogueElement.Count == EpisodeManager.Instanace.DialogueIdx)
        {
            EpisodeManager.Instanace.EpisodeIdx++;
            EpisodeManager.Instanace.DialogueIdx = 0;

            if(EpisodeManager.Instanace.EpisodeIdx == _selectEpisodeDataList.Count)
            {
                EpisodeManager.Instanace.EpisodeEndEvent?.Invoke();
                return;
            }
        }

        _selectDialogueElement = _selectEpisodeDataList[EpisodeManager.Instanace.EpisodeIdx].dialogueElement[EpisodeManager.Instanace.DialogueIdx];
        PhaseEventConnect();
        while (_selectEpisodeDataList[EpisodeManager.Instanace.EpisodeIdx].dialogueElement[EpisodeManager.Instanace.DialogueIdx].isLinker)
        {
            _selectDialogueElement = _selectEpisodeDataList[EpisodeManager.Instanace.EpisodeIdx].dialogueElement[EpisodeManager.Instanace.DialogueIdx];
            PhaseConnectStandard();
        }
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
        Debug.Log(1);
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
        EpisodeManager.Instanace.DialogueIdx++;
    }
}
