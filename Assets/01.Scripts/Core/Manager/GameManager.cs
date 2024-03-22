using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public string BeforeSceneName { get; private set; }

    public Action<int> LoadingProgressChanged { get; set; }
    private int _loadingProgress;
    private int LoadingProgress
    {
        get { return _loadingProgress;}
        set 
        { 
            if(value != _loadingProgress)
            {
                LoadingProgressChanged?.Invoke(value);
            }
            _loadingProgress = value; 
        }
    }

    [Header("Pooling")]
    [SerializeField] private PoolListSO _poolingList;
    [SerializeField] private Transform _poolingTrm;

    private void Awake()
    {
        PoolManager.Instance = new PoolManager(_poolingTrm);
        foreach (PoolingItem item in _poolingList.poolList)
        {
            PoolManager.Instance.CreatePool(item.prefab, item.type, item.count);
        }
    }

    public void ChangeScene(string sceneName)
    {
        BeforeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(LoadingProcessCo(sceneName));
    }

    public Scene GetCurrentSceneInfo()
    {
        return SceneManager.GetActiveScene();
    }

    private IEnumerator LoadingProcessCo(string sceneName)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            LoadingProgress = Mathf.CeilToInt(asyncOperation.progress * 100);
            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1);
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}