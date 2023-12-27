using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EpisodeDialogueDefine;
using System;

[Serializable]
public struct DialogueStandardElement
{
    public string name;
    public string sentence;
    public BackGroundType backGroundType;

    public DialogueStandardElement(string n, string s, BackGroundType bt)
    {
        name = n;
        sentence = s;
        backGroundType = bt;
    }
}

[Serializable]
public struct DialogueCharacterElement
{
    public CharacterType characterType;
    public FaceType faceType;
    public bool isActive;
    public bool isShake;
    public EmotionType emotionType;

    public DialogueCharacterElement(CharacterType ct,FaceType ft, bool ac, bool sh, EmotionType et)
    {
        characterType = ct;
        faceType = ft;
        isActive = ac;
        isShake = sh;
        emotionType = et;
    }
}

[System.Serializable]
public struct DialogueMoveElement
{
    public MoveType moveType;
    public ExitType exitTpe;

    public DialogueMoveElement(MoveType mt, ExitType et)
    {
        moveType = mt;
        exitTpe = et;
    }
}

[Serializable]
public struct DialogueProductElement
{
    public FadeOutType fadeType;

    public DialogueProductElement(FadeOutType ft)
    {
        fadeType = ft;
    }
}

[Serializable]
public struct DialogueElement
{
    public DialogueStandardElement standardElement;
    public DialogueCharacterElement characterElement;
    public DialogueMoveElement movementElement;
    public DialogueProductElement productElement;
    public bool isLinker;

    public DialogueElement(DialogueStandardElement  _sElement,
                    DialogueCharacterElement _cElement,
                    DialogueMoveElement _mElement,
                    DialogueProductElement   _pElement,
                    bool _linker)
    {
        standardElement = _sElement;
        characterElement = _cElement;
        movementElement = _mElement;
        productElement = _pElement;
        isLinker = _linker;
    }
}

[CreateAssetMenu(menuName = "SO/Episode/Dialogue")]
public class EpisodeData : LoadableData
{
    public List<DialogueElement> dialogueElement = new List<DialogueElement>();

    public void GeneratDialogueData()
    {
        dialogueElement.Clear();
        for(int i = 0; i < generateData.Count; i++)
        {
            dialogueElement.Add
            (
                new DialogueElement
                (
                    AllocateSE(generateData[i].str[0], generateData[i].str[1], generateData[i].str[2]),
                    AllocateCE(generateData[i].str[4], generateData[i].str[5], generateData[i].str[6], generateData[i].str[7], generateData[i].str[10]),
                    AllocateME(generateData[i].str[8], generateData[i].str[9]),
                    AllocatePE(generateData[i].str[3]),
                    generateData[i].str[1].Contains("link")
                )
            );
        }
        Debug.Log("Complete DataReading!!");
    }

    private DialogueStandardElement AllocateSE(string n, string s, string bt)
    {
        return new DialogueStandardElement
            (
                n, s,
                (BackGroundType)Enum.Parse(typeof(BackGroundType), bt)
            );
    }

    private DialogueCharacterElement AllocateCE(string ct, string ft, string ac, string sh, string et)
    {
        return new DialogueCharacterElement
            (
                (CharacterType)Enum.Parse(typeof(CharacterType), ct),
                (FaceType)Enum.Parse(typeof(FaceType), ft),
                Convert.ToBoolean(ac),
                Convert.ToBoolean(sh),
                (EmotionType)Enum.Parse(typeof(EmotionType), et)
            ) ; 
    }

    private DialogueMoveElement AllocateME(string mt, string et)
    {
        return new DialogueMoveElement
            (
                (MoveType)Enum.Parse(typeof(MoveType), mt),
                (ExitType)Enum.Parse(typeof(ExitType), et)
            );
    }

    private DialogueProductElement AllocatePE(string ft)
    {
        return new DialogueProductElement((FadeOutType)Enum.Parse(typeof(FadeOutType), ft));
    }
}

[CustomEditor(typeof(EpisodeData))]
public class EpisodeLoader : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EpisodeData episodeData = (EpisodeData)target;
        LoadableData ld = episodeData;
        if (GUILayout.Button("EpisodeDataReading"))
        {
            Debug.Log("DataReading Start . . .");
            episodeData.GeneratDialogueData();
        }
        if (GUILayout.Button("DataGenerate"))
        {
            Debug.Log("DataGenerate Start . . .");
            ld.Generate();
        }
    }
}
