using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using UnityEngine.Events;
using System;
using System.Text.RegularExpressions;

[Serializable]
public struct Data
{
    public string[] str;
    public Data(string[] s)
    {
        str = s;
    }
}

public class LoadableData : ScriptableObject
{
    [SerializeField] protected List<Data> generateData = new List<Data>();
    public string sheetUrI;
    public string sheetPage;
    public string cellRange;

    private void GenerateData(List<GSTU_Cell> list, int start, int end)
    {
        int len = end - start + 1;
        List<GSTU_Cell> newList = list.GetRange(start, len);
        List<string> arr =
            newList.ConvertAll(new Converter<GSTU_Cell, string>((GSTU_Cell x) => x.value));
        generateData.Add(new Data(arr.ToArray()));

    }

    public void Generate()
    {
        UpdateStatas(UpdateMethodOne);
    }

    private void UpdateStatas(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(sheetUrI, sheetPage), callback, mergedCells);
    }

    private void UpdateMethodOne(GstuSpreadSheet ss)
    {
        string[] range = cellRange.Split(':');
        if(range.Length != 2)
        {
            Debug.LogError("Error : CellRange is not proper. plz re Write CellRange!");
        }

        RangeCalculator rangeCal = new RangeCalculator(range[0], range[1]);
        Vector2[] callingRanges = rangeCal.CalculateCellRange();

        generateData.Clear();
        for (int i = (int)callingRanges[0].y; i < (int)callingRanges[1].y + 1; i++)
        {
            GenerateData(ss.rows[i], (int)callingRanges[0].x, (int)callingRanges[1].x);
        }
    }
}

public class RangeCalculator
{
    private string[] _cellMarks = new string[2];
    private char[] _wordRangeGroup = new char[2];
    private int[] _numberRangeGroup = new int[2];

    private const string _wordGroupBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const int _maxColumnLength = 4000;

    public RangeCalculator(string cellMark_1, string cellMark_2)
    {
        Debug.Log($"{cellMark_1}, {cellMark_2}");

        _cellMarks[0] = cellMark_1;
        _cellMarks[1] = cellMark_2;

        for(int i = 0; i < _cellMarks.Length; i++)
        {
            MatchCollection matches = Regex.Matches(_cellMarks[i], "[A-Za-z]+|[0-9]+");
            _wordRangeGroup[i] = matches[0].Value[0]; // 짜피 Z안넘어감~ 몰라래후~
            _numberRangeGroup[i] = Convert.ToInt32(matches[1].Value);
        }
    }

    // 행 시작 인덱스 = 00, 열 시작 인덱스 = 01,
    // 행 끝나는 인덱스 = 10, 열 끝나는 인덱스 11
    public Vector2[] CalculateCellRange()
    {
        Vector2[] value = new Vector2[2];

        for(int t = 0; t < value.Length; t++)
        {
            for (int i = 0; i < _wordGroupBase.Length; i++)
            {
                if (_wordRangeGroup[t] == _wordGroupBase[i])
                {
                    value[t].x = i;
                    break;
                }
            }

            int left = 1;
            int right = _maxColumnLength;
            int target = _numberRangeGroup[t];

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (mid == target)
                {
                    value[t].y = mid;
                    break;
                }

                if (mid < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }
        return value;
    }
}