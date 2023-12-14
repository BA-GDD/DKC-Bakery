using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadableData : ScriptableObject
{
    [SerializeField] protected List<string[]> generateData = new List<string[]>();
    public string sheetUrl;
    public string cellRange;
    public string sheetData;

    public IEnumerator StartLoadData()
    {
        sheetUrl += ("export?format=tsv&range=" + cellRange);
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
            generateData.Add(columns);
        }
        for(int i = 0; i < generateData.Count; i++)
        {
            for(int j = 0; j < generateData[i].Length; j++)
            {
                Debug.Log(generateData[i][j]);
            }
            Debug.Log("ÁÙ¹Ù²Þ");
        }
        Debug.Log(generateData.Count);
    }
}