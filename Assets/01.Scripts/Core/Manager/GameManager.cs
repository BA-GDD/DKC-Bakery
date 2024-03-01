using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Player _player;
    public Transform PlayerTrm => Player.transform;
    public Player Player
    {
        get
        {
            if (_player != null) return _player;
            _player = FindObjectOfType<Player>();
            return _player;
        }
    }
    public string beforeSceneName;

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
        beforeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(LoadingProcessCo(sceneName));
    }

    private IEnumerator LoadingProcessCo(string sceneName)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    #region Debug
    [Header("Debug")]
    [SerializeField] private InputReader _inputReader;
    private void Update()
    {
        _inputReader?.UpdateBuffer();
    }
    #endregion
}