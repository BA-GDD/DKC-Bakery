using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [HideInInspector]
    public SceneUI currentSceneUI;

    private void Awake()
    {
        // Debug
        GetSceneUI<MapSceneUI>();
    }

    public void GetSceneUI<T>() where T : SceneUI
    {
        currentSceneUI = GameObject.Find(typeof(T).Name).GetComponent<T>();
    }
}
