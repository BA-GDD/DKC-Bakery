#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System;

public class SheetDataLoader 
{
    private void AddData<T>(DataLoadType type, T asset, string[] dataArr, string line, string assetPath) where T : LoadableData
    {
        asset.AddData(dataArr);
        AssetDatabase.SaveAssets();
    }

    public void HandleData<T>(string data, DataLoadType type, out int lineNum) where T : LoadableData
    {
        string assetPath = $"Assets/05.SO/SheetData/{type}.asset";

        T asset = ScriptableObject.CreateInstance<T>();
        asset.Type = type;

        string[] lines = data.Split("\n");

        for (lineNum = 1; lineNum < lines.Length; lineNum++)
        {
            string[] dataArr = lines[lineNum].Split("\r");
            dataArr = dataArr[0].Split("\t");
            switch (type)
            {
                case DataLoadType.LikebilityData:
                    AddData(type, asset as LikebilityData, dataArr, lines[lineNum], assetPath);
                    break;
            }
        }

        string fileName = AssetDatabase.GenerateUniqueAssetPath(assetPath);

        AssetDatabase.DeleteAsset(assetPath);
        AssetDatabase.CreateAsset(asset, fileName);

        AssetDatabase.Refresh();
    }

    public IEnumerator GetDataFromSheet(string documentID, string sheetID, Action<bool, string> Process)
    {
        if (documentID == "" || sheetID == "" || documentID == "sheet ID" || sheetID == "page name")
        {
            Process?.Invoke(false, "ERROR: ��Ȯ�� ���� �Է��ϼ���");
            yield break;
        }

        string url = $"https://docs.google.com/spreadsheets/d/{documentID}/export?format=tsv&gid={sheetID}";
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.ConnectionError || req.responseCode != 200)
        {
            Process?.Invoke(false, "ERROR: ���� �ҷ����� �� ���� �߻�");
            yield break;
        }

        Process?.Invoke(true, req.downloadHandler.text);
    }

#endif
}
