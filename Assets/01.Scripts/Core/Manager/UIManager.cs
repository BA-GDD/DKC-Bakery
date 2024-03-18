using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIDefine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private SceneUI[] _screenElementGroup;

    private Dictionary<UIScreenType, SceneUI> _sceneUIDic = new Dictionary<UIScreenType, SceneUI>();

    private UIScreenType _currentSceneUIType;
    public UIScreenType CurrentSceneUI => _currentSceneUIType;
    private SceneUI _currentSceneUIObject;

    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            if (_canvas != null) return _canvas;
            Canvas findCanvas = FindObjectOfType<Canvas>();
            return findCanvas;
        }
        set
        {
            _canvas = value;
        }
    }
    public Transform CanvasTrm => Canvas.transform;

    private void Awake()
    {
        foreach(SceneUI su in _screenElementGroup)
        {
            _sceneUIDic.Add(su.ScreenType, su);
        }

        SceneManager.sceneLoaded += ChangeSceneUIOnChangeScene;
        ChangeSceneUIOnChangeScene(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    public T GetSceneUI<T>() where T : SceneUI
    {
        return (T)FindFirstObjectByType(typeof(T));
    }
    
    public void ChangeSceneUIOnChangeScene(Scene updateScene, LoadSceneMode mode)
    {
        _currentSceneUIType = (UIScreenType)updateScene.buildIndex;

        if(_currentSceneUIObject != null)
        {
            _currentSceneUIObject.SceneUIEnd();
            Destroy(_currentSceneUIObject.gameObject);
        }

        if (_sceneUIDic.ContainsKey(_currentSceneUIType))
        {
            SceneUI suObject = Instantiate(_sceneUIDic[_currentSceneUIType], CanvasTrm);
            suObject.gameObject.name = _sceneUIDic[_currentSceneUIType].gameObject.name + "_MAESTRO_[SceneUI]_";
            suObject.SceneUIStart();

            _currentSceneUIObject = suObject;
        }
    }
}
