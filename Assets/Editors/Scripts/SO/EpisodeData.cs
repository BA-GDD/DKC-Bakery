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

    public DialogueCharacterElement(CharacterType ct,FaceType ft, bool ac, bool sh)
    {
        characterType = ct;
        faceType = ft;
        isActive = ac;
        isShake = sh;
    }
}

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

    public DialogueElement(DialogueStandardElement  _sElement,
                    DialogueCharacterElement _cElement,
                    DialogueMoveElement _mElement,
                    DialogueProductElement   _pElement)
    {
        standardElement = _sElement;
        characterElement = _cElement;
        movementElement = _mElement;
        productElement = _pElement;
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
                    AllocateSE(generateData[i][0], generateData[i][1], generateData[i][2]),
                    AllocateCE(generateData[i][4], generateData[i][5], generateData[i][6], generateData[i][7]),
                    AllocateME(generateData[i][8], generateData[i][9]),
                    AllocatePE(generateData[i][3])
                )
            );
        }
    }

    private DialogueStandardElement AllocateSE(string n, string s, string bt)
    {
        return new DialogueStandardElement
            (
                n, s,
                (BackGroundType)Enum.Parse(typeof(BackGroundType), bt)
            );
    }

    private DialogueCharacterElement AllocateCE(string ct, string ft, string ac, string sh)
    {
        return new DialogueCharacterElement
            (
                (CharacterType)Enum.Parse(typeof(CharacterType), ct),
                (FaceType)Enum.Parse(typeof(FaceType), ft),
                Convert.ToBoolean(Convert.ToInt16(ac)),
                Convert.ToBoolean(Convert.ToInt16(sh))
            ); 
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
        if (GUILayout.Button("EpisodeDataReading"))
        {
            episodeData.GeneratDialogueData();
        }
    }
}
