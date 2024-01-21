using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIDefine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private SceneUI[] _screenElementGroup;
    private Dictionary<UIScreenType, List<SceneUI>> _sceneUIDic = new Dictionary<UIScreenType, List<SceneUI>>();
    private UIScreenType _currentSceneUIType;
    public UIScreenType CurrentSceneUI => _currentSceneUIType;
    private List<SceneUI> _currentSceneUIObjectList = new List<SceneUI>();

    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            if (_canvas != null) return _canvas;
            return FindObjectOfType<Canvas>();
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
            if(_sceneUIDic.ContainsKey(su.ScreenType))
            {
                List<SceneUI> sceneUIList = new List<SceneUI>();
                sceneUIList.Add(su);
                _sceneUIDic.Add(su.ScreenType, sceneUIList);

                continue;
            }

            _sceneUIDic[su.ScreenType].Add(su);
        }

        SceneManager.sceneLoaded += ChangeSceneUIOnChangeScene;
        ChangeSceneUIOnChangeScene(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    public T GetSceneUI<T>() where T : SceneUI
    {
        return FindFirstObjectByType(typeof(T)) as T;
    }

    public SceneUI GetSceneUI(string sceneName)
    {
        return FindFirstObjectByType(Type.GetType($"{sceneName}UI")) as SceneUI;
    }

    public void ChangeSceneUIOnChangeScene(Scene updateScene, LoadSceneMode mode)
    {
        _currentSceneUIType = (UIScreenType)updateScene.buildIndex;

        if(_currentSceneUIObjectList.Count > 0)
        {
            foreach (SceneUI su in _currentSceneUIObjectList)
            {
                Destroy(su.gameObject);
            }
            _currentSceneUIObjectList.Clear();
        }

        foreach(SceneUI su in _sceneUIDic[_currentSceneUIType])
        {
            GameObject suObject = Instantiate(su.gameObject, CanvasTrm);
            suObject.name = su.gameObject.name + "__[SceneUI]";
        }
    }
}
