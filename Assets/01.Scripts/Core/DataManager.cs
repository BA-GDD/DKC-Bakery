using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoSingleton<DataManager>
{
    private List<string> _datakeyList = new List<string>();
    private readonly string _dataKeyFilePath = Path.Combine(Application.dataPath, "DataKeys.json");

    private void Awake()
    {
        string[] keyArr = File.ReadAllText(_dataKeyFilePath).Split(",");
        int saveFileCount = keyArr.Length - 1;

        for(int i = 0; i < saveFileCount; i++)
        {
            _datakeyList.Add(keyArr[i]);
        }
    }

    public void SaveData(CanSaveData saveData)
    {
        string dataKey = nameof(saveData);

        if(!IsHaveData(dataKey))
        {
            File.WriteAllText(_dataKeyFilePath, $"{dataKey},");
            _datakeyList.Add(dataKey);
        }

        File.WriteAllText(GetFilePath(dataKey), JsonUtility.ToJson(saveData));
    }

    public T LoadData<T>() where T : CanSaveData
    {
        string dataKey = nameof(T);

        if (!IsHaveData(dataKey))
        {
            Debug.LogError($"Error! No exit data key!! Key name : {dataKey}");
            return default(T);
        }

        return JsonUtility.FromJson<T>(File.ReadAllText(GetFilePath(dataKey)));
    }

    public bool IsHaveData(CanSaveData saveData)
    {
        return _datakeyList.Contains(nameof(saveData));
    }

    public bool IsHaveData(string dataKey)
    {
        return _datakeyList.Contains(nameof(dataKey));
    }

    private string GetFilePath(string dataKey)
    {
        return Path.Combine(Application.dataPath, $"{dataKey}.json");
    }
}
