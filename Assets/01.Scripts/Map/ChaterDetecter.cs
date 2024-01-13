using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChapterDefine;

public class ChaterDetecter : MonoBehaviour
{
    [SerializeField] private ChapterType _myChapterType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        MapManager.Instanace.CurrentChapter = _myChapterType;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MapManager.Instanace.CurrentChapter = ChapterType.None;
    }
}
