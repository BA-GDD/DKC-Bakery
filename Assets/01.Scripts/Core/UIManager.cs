using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    [HideInInspector]
    public SceneUI currentSceneUI;

    private void Awake()
    {
        SceneManager.activeSceneChanged += ChangeSceneUIOnChangeScene;

        // Debug
        currentSceneUI = GetSceneUI<MapSceneUI>();
    }

    public T GetSceneUI<T>() where T : SceneUI
    {
        return FindFirstObjectByType(typeof(T)) as T;
    }

    public SceneUI GetSceneUI(string sceneName)
    {
        return FindFirstObjectByType(Type.GetType($"{sceneName}UI")) as SceneUI;
    }

    public SceneUI GetSceneUI()
    {
        return FindFirstObjectByType<SceneUI>();
    }

    public void ChangeSceneUIOnChangeScene(Scene previousScene, Scene nextScene)
    {
        currentSceneUI = GetSceneUI(nextScene.name);
    }
}
