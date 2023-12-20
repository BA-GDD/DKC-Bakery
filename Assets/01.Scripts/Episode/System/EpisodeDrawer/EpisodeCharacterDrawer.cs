using System.Collections.Generic;
using UnityEngine;
using EpisodeDialogueDefine;
using System;

public class EpisodeCharacterDrawer : MonoBehaviour
{
    [SerializeField] private CharacterStandard[] _characterStandArr;
    private Dictionary<CharacterType, CharacterStandard> _characterSelectDictionary = new Dictionary<CharacterType, CharacterStandard>();
    private CharacterStandard _selectCharacter;

    private void Awake()
    {
        foreach(CharacterType ct in Enum.GetValues(typeof(CharacterType)))
        {
            _characterSelectDictionary.Add(ct, _characterStandArr[(int)ct]);
        }
    }

    public void HandleCharacterDraw(CharacterType ct, FaceType faceType , bool isActive, bool isShake)
    {
        _selectCharacter = _characterSelectDictionary[ct];
        _selectCharacter.SetFace(faceType);
        _selectCharacter.SetActive(isActive);

        if (!isShake) return;
        _selectCharacter.CharacterShake();
    }

    public void HandleCharacterMoveDraw(CharacterType ct, MoveType moveType, ExitType exitType)
    {
        _selectCharacter = _characterSelectDictionary[ct];
        _selectCharacter.MoveCharacter(moveType);
        _selectCharacter.ExitCharacter(exitType);
    }
}
