using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EpisodeDialogueDefine;
using System;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;

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
    public FaceType faceType;
    public bool isShake;
    public EmotionType emotionType;

    public DialogueCharacterElement(FaceType ft, bool sh, EmotionType et)
    {
        faceType = ft;
        isShake = sh;
        emotionType = et;
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
public struct CaptureElement
{
    public bool isActive;
    public Vector2 movePosition;

    public CaptureElement(bool _isActive, Vector2 _movePosition)
    {
        isActive = _isActive;
        movePosition = _movePosition;
    }
}


[Serializable]
public struct DialogueElement
{
    public DialogueStandardElement standardElement;
    public DialogueCharacterElement characterElement;
    public DialogueProductElement productElement;
    public CaptureElement captureElement;
    public bool isLinker;

    public DialogueElement(DialogueStandardElement  _sElement,
                    DialogueCharacterElement _cElement,
                    DialogueProductElement   _pElement,
                    CaptureElement _capElement,
                    bool _linker)
    {
        standardElement = _sElement;
        characterElement = _cElement;
        productElement = _pElement;
        captureElement = _capElement;
        isLinker = _linker;
    }
}



#if UNITY_EDITOR
[CreateAssetMenu(menuName = "SO/Episode/Dialogue")]
#endif
public class EpisodeData : LoadableData
{
    public int CurrentCaptureIdx;
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
                    AllocateCE(generateData[i].str[4], generateData[i].str[5], generateData[i].str[6]),
                    AllocatePE(generateData[i].str[3]),
                    new CaptureElement(false, Vector2.zero),
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
    private DialogueCharacterElement AllocateCE(string ft, string sh, string et)
    {
        return new DialogueCharacterElement
            (
                (FaceType)Enum.Parse(typeof(FaceType), ft),
                Convert.ToBoolean(sh),
                (EmotionType)Enum.Parse(typeof(EmotionType), et)
            ) ; 
    }
    private DialogueProductElement AllocatePE(string ft)
    {
        return new DialogueProductElement((FadeOutType)Enum.Parse(typeof(FadeOutType), ft));
    }

    public void CaptureCharacterElement(CharacterType characterType)
    {
        string cPath = "ManagerGroup/EpisodeManager/EpisodeGroup/UICANVAS/CharacterGroup";

        if (characterType == CharacterType.tart)
        {
            cPath += "/Tart";
        }
        else
        {
            cPath += "/Mawang";
        }
        CharacterStandard selectCharacter = GameObject.Find(cPath).GetComponent<CharacterStandard>();
        CaptureElement ce = new CaptureElement(selectCharacter.gameObject.activeSelf, selectCharacter.transform.localPosition);
        DialogueElement de = dialogueElement[CurrentCaptureIdx];
        de.captureElement = ce;
        dialogueElement[CurrentCaptureIdx] = de;
        CurrentCaptureIdx++;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EpisodeData))]
public class EpisodeLoader : Editor
{
    public CharacterType CaptureCharacterType;

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

        GUILayout.Label("캡쳐할 캐릭터 선택");
        GUIContent[] enumOptions = new GUIContent[Enum.GetNames(typeof(CharacterType)).Length];
        for (int i = 0; i < enumOptions.Length; i++)
        {
            enumOptions[i] = new GUIContent(Enum.GetNames(typeof(CharacterType))[i]);
        }
        CaptureCharacterType = (CharacterType)GUILayout.SelectionGrid((int)CaptureCharacterType, enumOptions, 1);

        if (GUILayout.Button("CaptureCharacterPose"))
        {
            episodeData.CaptureCharacterElement(CaptureCharacterType);
            Debug.Log("Cheez :)");
        }
    }
}
#endif
