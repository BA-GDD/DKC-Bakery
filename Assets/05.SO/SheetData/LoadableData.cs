using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class SheetData
{
    public  string[] rowData;

    public SheetData(string[] row)
    {
        rowData = row;
    }
}

public class LoadableData : ScriptableObject
{
    [SerializeField] protected List<SheetData> generateData = new List<SheetData>();
    public string sheetUrl;
    public string sheetData;

    public IEnumerator StartLoadData()
    {
        using(UnityWebRequest www = UnityWebRequest.Get(sheetUrl))
        {
            yield return www.SendWebRequest();

            if(www.isDone)
            {
                sheetData = www.downloadHandler.text;
            }
        }
        GenerateData();
    }

    private void GenerateData()
    {
        generateData.Clear();

        string[] rows = sheetData.Split('\n');
        for (int i = 0; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');
            generateData.Add(new SheetData(columns));
        }
        for(int i = 0; i < generateData.Count; i++)
        {
            for(int j = 0; j < generateData[i].rowData.Length; j++)
            {
                Debug.Log(generateData[i].rowData[j]);
            }
            Debug.Log("ÁÙ¹Ù²Þ");
        }
        Debug.Log(generateData.Count);
    }
}