using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EpisodeDialogueDefine;
using System;

public struct LogData
{
    public Sprite characterSprite;
    public string characterName;
    public string characterSyntex;

    public LogData(Sprite img, string name, string syntex)
    {
        characterSprite = img;
        characterName = name;
        characterSyntex = syntex;
    }
}

public class EpisodeManager : MonoBehaviour
{
    private static EpisodeManager _instance;
    public static EpisodeManager Instanace
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<EpisodeManager>();
            if (_instance == null)
            {
                Debug.LogError("Not Exist EpisodeManager");
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject _episodeGroup;
    [SerializeField] private UnityEvent<EpisodeData> _episodeStartEvent;
    [SerializeField] private UnityEvent _nextDialogueEvent;
    [SerializeField] private UnityEvent<bool> _activeSyntexPanel;
    [SerializeField] private Sprite[] _characterSprite;

    public Action EpisodeEndEvent { get; set; }

    [HideInInspector] public int DialogueIdx { get; set; }
    [HideInInspector] public int EpisodeIdx { get; set; }
    [HideInInspector] public List<LogData> dialogueLog = new List<LogData>();
    [HideInInspector] public bool isTextInTyping;

    public void SetPauseEpisode(bool isPause)
    {
        ActiveUIPlanel(!isPause);
    }

    public void SetPauseEpisode(bool isPause, Action callback)
    {
        ActiveUIPlanel(!isPause);
        callback?.Invoke();
    }

    public void StartEpisode(EpisodeData data)
    {
        ActiveUIPlanel(true);
        _episodeStartEvent?.Invoke(data);
    }

    public void ActiveUIPlanel(bool isActive)
    {
        _episodeGroup.SetActive(isActive);
    }

    public void AddDialogeLogData(CharacterType ct, string name, string syntex)
    {
        LogData logData = new LogData(_characterSprite[(int)ct], name, syntex);
        dialogueLog.Add(logData);
    }

    public void NextDialogue()
    {
        _nextDialogueEvent?.Invoke();
    }

    public void ActiveSyntexPanel(bool isActive)
    {
        _activeSyntexPanel?.Invoke(isActive);
    }
}
