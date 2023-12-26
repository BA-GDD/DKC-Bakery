using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EpisodeDialogueDefine;

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

    [SerializeField] private UnityEvent _nextDialogueEvent;
    [SerializeField] private UnityEvent<bool> _activeSyntexPanel;
    [SerializeField] private Sprite[] _characterSprite;

    [HideInInspector] public int dialogueIdx;
    [HideInInspector] public List<LogData> dialogueLog = new List<LogData>();
    [HideInInspector] public bool isTextInTyping;

    [SerializeField] private PoolListSO _poolList;

    [SerializeField] private PoolManager _poolManager;

    private void Start()
    {
        _poolManager.Init(_poolList);
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
