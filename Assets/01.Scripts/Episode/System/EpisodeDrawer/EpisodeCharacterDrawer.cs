using System.Collections.Generic;
using UnityEngine;
using EpisodeDialogueDefine;
using System;

[Serializable]
public struct CharacterElementGroup
{
    public CharacterStandard characterStand;
    public RectTransform[] emotionTrm;
}

[Serializable]
public struct EmotionElementGroup
{
    public Sprite elementImg;
    public AnimationClip elementClip;
}

public class EpisodeCharacterDrawer : MonoBehaviour
{
    [SerializeField] private CharacterElementGroup[] _characterGroupArr;
    [SerializeField] private EmotionElementGroup[] _emotionGroupArr;
    private Dictionary<CharacterType, CharacterStandard> _characterSelectDictionary = new Dictionary<CharacterType, CharacterStandard>();
    private Dictionary<CharacterType, MoveType> _characterPosSaveDic = new Dictionary<CharacterType, MoveType>();
    private CharacterStandard _selectCharacter;

    private void Awake()
    {
        foreach(CharacterType ct in Enum.GetValues(typeof(CharacterType)))
        {
            _characterSelectDictionary.Add(ct, _characterGroupArr[(int)ct].characterStand);
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
        SaveCharacterPos(ct, moveType);

        _selectCharacter = _characterSelectDictionary[ct];
        _selectCharacter.MoveCharacter(moveType);
        _selectCharacter.ExitCharacter(exitType);
    }

    public void HandleDialogueEffectDraw(CharacterType ct, EmotionType emo)
    {
        if (emo == EmotionType.None) return;

        DialogueEffect de = PoolManager.Instance.Pop(PoolingType.DialogueEffect) as DialogueEffect;
        int idx = Mathf.Clamp((int)_characterPosSaveDic[ct] - 1, 0, 1);
        de.transform.parent = _characterGroupArr[(int)ct].emotionTrm[idx];
        de.transform.localPosition = Vector3.zero;

        EmotionElementGroup eg = _emotionGroupArr[(int)emo - 1];
        de.StartEffect(eg.elementImg, eg.elementClip, _characterPosSaveDic[ct]);
    }

    private void SaveCharacterPos(CharacterType ct, MoveType mt)
    {
        if (_characterPosSaveDic.ContainsKey(ct))
        {
            _characterPosSaveDic[ct] = mt;
            return;
        }
        _characterPosSaveDic.Add(ct, mt);
    }
}
