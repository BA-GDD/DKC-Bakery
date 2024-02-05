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
            Canvas findCanvas = FindObjectOfType<Canvas>();

            if(findCanvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                findCanvas.worldCamera = Camera.main;
            }

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
            if(_sceneUIDic.ContainsKey(su.ScreenType))
            {
                List<SceneUI> sceneUIList = new List<SceneUI>();
                sceneUIList.Add(su);
                _sceneUIDic.Add(su.ScreenType, sceneUIList);

                continue;
            }

            _sceneUIDic[su.ScreenType].Add(su);
        }

        //SceneManager.sceneLoaded += ChangeSceneUIOnChangeScene;
        //ChangeSceneUIOnChangeScene(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    public T GetSceneUI<T>() where T : SceneUI
    {
        return (T)FindFirstObjectByType(typeof(T));
    }
    
    public void ChangeSceneUIOnChangeScene(Scene updateScene, LoadSceneMode mode)
    {
        _currentSceneUIType = (UIScreenType)updateScene.buildIndex;

        if(_currentSceneUIObjectList.Count > 0)
        {
            foreach (SceneUI su in _currentSceneUIObjectList)
            {
                su.SceneUIEnd();
                Destroy(su.gameObject);
            }
            _currentSceneUIObjectList.Clear();
        }

        foreach(SceneUI su in _sceneUIDic[_currentSceneUIType])
        {
            SceneUI suObject = Instantiate(su, CanvasTrm);
            suObject.gameObject.name = su.gameObject.name + "_MAESTRO_[SceneUI]_";
            suObject.SceneUIStart();

            _currentSceneUIObjectList.Add(su);
        }
    }
}
