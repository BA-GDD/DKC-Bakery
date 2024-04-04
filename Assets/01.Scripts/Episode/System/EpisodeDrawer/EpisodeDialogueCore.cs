using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EpisodeDialogueDefine;

public class EpisodeDialogueCore : MonoBehaviour
{
    [SerializeField] private List<EpisodeData> _selectEpisodeDataList;
    private DialogueElement _selectDialogueElement;
    private EpisodeManager epiManager;

    [SerializeField] private UnityEvent<string, string, BackGroundType> StandardDrawEvent;
    [SerializeField] private UnityEvent<FadeOutType> ProductionDrawEvent;
    [SerializeField] private UnityEvent<CharacterType, FaceType, bool, bool> CharacterDrawEvent;
    [SerializeField] private UnityEvent<CharacterType, Vector2> CharacterMoveEvent;
    [SerializeField] private UnityEvent<CharacterType, EmotionType> CharacterEmotionEvent;

    public void HandleEpisodeStart(List<EpisodeData> episodeDataList)
    {
        epiManager = EpisodeManager.Instanace;
        _selectEpisodeDataList = episodeDataList;
        HandleNextDialogue();
    }

    public void HandleNextDialogue()
    {
        if(epiManager.DialogueIdx == epiManager.PauseIdx[epiManager.PuaseCount])
        {
            epiManager.SetPauseEpisode(true);
            epiManager.PuaseCount++;
            return;
        }

        if(_selectEpisodeDataList[epiManager.EpisodeIdx].dialogueElement.Count == epiManager.DialogueIdx)
        {
            epiManager.EpisodeIdx++;
            epiManager.DialogueIdx = 0;

            if(epiManager.EpisodeIdx == _selectEpisodeDataList.Count)
            {
                epiManager.EpisodeEndEvent?.Invoke();
                return;
            }
        }

        _selectDialogueElement = _selectEpisodeDataList[epiManager.EpisodeIdx].dialogueElement[epiManager.DialogueIdx];
        PhaseEventConnect();
        while (_selectEpisodeDataList[epiManager.EpisodeIdx].dialogueElement[epiManager.DialogueIdx].isLinker)
        {
            _selectDialogueElement = _selectEpisodeDataList[epiManager.EpisodeIdx].dialogueElement[epiManager.DialogueIdx];
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
        CharacterType characterType = EpiswordMaster.GetCharacterTypeByName(_selectDialogueElement.standardElement.name);

        ProductionDrawEvent?.Invoke(_selectDialogueElement.productElement.fadeType);

        epiManager.AddDialogeLogData(              characterType,
                                                   _selectDialogueElement.standardElement.name,
                                                   _selectDialogueElement.standardElement.sentence);

        CharacterDrawEvent?.Invoke(characterType,
                                   _selectDialogueElement.characterElement.faceType,
                                   _selectDialogueElement.captureElement.isActive,
                                   _selectDialogueElement.characterElement.isShake);

        CharacterMoveEvent?.Invoke(characterType,
                                   _selectDialogueElement.captureElement.movePosition);

        CharacterEmotionEvent?.Invoke(characterType,
                                      _selectDialogueElement.characterElement.emotionType);

        
        epiManager.DialogueIdx++;
    }
}
