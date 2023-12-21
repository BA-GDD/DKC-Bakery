using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public bool IsTextInTyping;

    public void NextDialogue()
    {
        _nextDialogueEvent?.Invoke();
    }

    public void ActiveSyntexPanel(bool isActive)
    {
        _activeSyntexPanel?.Invoke(isActive);
    }
}
