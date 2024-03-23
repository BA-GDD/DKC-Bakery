using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIDefine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private SceneUI[] _screenElementGroup;

    private Dictionary<SceneType, SceneUI> _sceneUIDic = new Dictionary<SceneType, SceneUI>();

    private SceneType CurrentSceneType => GameManager.Instance.CurrentSceneType;
    private SceneUI _currentSceneUIObject;

    public Canvas Canvas { get; private set; }
    public Transform CanvasTrm => Canvas.transform;

    private void Awake()
    {
        Canvas = GetComponentInChildren<Canvas>();
        foreach(SceneUI su in _screenElementGroup)
        {
            _sceneUIDic.Add(su.ScreenType, su);
        }

        SceneManager.sceneLoaded += ChangeSceneUIOnChangeScene;
    }

    public T GetSceneUI<T>() where T : SceneUI
    {
        return (T)FindFirstObjectByType(typeof(T));
    }
    
    public void ChangeSceneUIOnChangeScene(Scene updateScene, LoadSceneMode mode)
    {
        if(_currentSceneUIObject != null)
        {
            _currentSceneUIObject.SceneUIEnd();
            Destroy(_currentSceneUIObject.gameObject);
        }

        if (_sceneUIDic.ContainsKey(CurrentSceneType))
        {
            Debug.Log(CurrentSceneType);
            SceneUI suObject = Instantiate(_sceneUIDic[CurrentSceneType], CanvasTrm);
            suObject.gameObject.name = _sceneUIDic[CurrentSceneType].gameObject.name + "_MAESTRO_[SceneUI]_";
            suObject.SceneUIStart();

            _currentSceneUIObject = suObject;
        }
    }
}
