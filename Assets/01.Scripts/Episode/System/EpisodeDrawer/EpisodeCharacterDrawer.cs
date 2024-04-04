using System.Collections.Generic;
using UnityEngine;
using EpisodeDialogueDefine;
using System;
using UnityEngine.Events;

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
    [SerializeField] private Transform _emotionTrm;
    [SerializeField] private CharacterElementGroup[] _characterGroupArr;
    [SerializeField] private EmotionElementGroup[] _emotionGroupArr;
    private Dictionary<CharacterType, CharacterStandard> _characterSelectDictionary = new Dictionary<CharacterType, CharacterStandard>();
    private CharacterStandard _selectCharacter;
    private SoundSelecter _episodeSounder;

    private void Awake()
    {
        _episodeSounder = transform.parent.Find("EpisodeSounder").GetComponent<SoundSelecter>();
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

    public void HandleCharacterMoveDraw(CharacterType ct, Vector2 movePos)
    {
        _selectCharacter = _characterSelectDictionary[ct];
        _selectCharacter.MoveCharacter(movePos);
    }

    public void HandleDialogueEffectDraw(CharacterType ct, EmotionType emo)
    {
        if (emo == EmotionType.None) return;

        DialogueEffect de = PoolManager.Instance.Pop(PoolingType.DialogueEffect) as DialogueEffect;

        EmotionElementGroup eg = _emotionGroupArr[(int)emo - 1];
        de.transform.parent = _emotionTrm;
        de.transform.localScale = Vector3.one;
        de.StartEffect(eg.elementImg, eg.elementClip, _characterSelectDictionary[ct].transform.localPosition);

        SFXType st = emo == EmotionType.Sparkle ? SFXType.sparcle : SFXType.bubble;
        _episodeSounder.HandleOutputSFX(st);
    }
}
