using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadableData : ScriptableObject
{
    private string sheetData;
    [SerializeField] private string sheetRange;
    protected List<string[]> generateData = new List<string[]>();
    public string sheetUrl;

    public IEnumerator StartLoadData()
    {
        sheetData += ("export?format=tsv&range =" + sheetRange);
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
        string[] rows = sheetData.Split('\n');
        for (int i = 0; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');
            generateData.Add(columns);
        }
    }
}