using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsumegoSystem : MonoBehaviour
{
<<<<<<< HEAD
    public TsumegoInfo CurTsumegoInfo { get; set; }
=======
    public TsumegoInfo CurTsumegoInfo;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CheckClear();
        }
    }
>>>>>>> parent of 8b20a26 (0321 ë¨¸ì§€ ì „ ì»¤ë°‹)

    public void CheckClear()
    {
        foreach(var condition in CurTsumegoInfo.Conditions)
        {
            if (!condition.CheckCondition())
            {
<<<<<<< HEAD
=======
                // ½ÇÆÐ
>>>>>>> parent of 8b20a26 (0321 ë¨¸ì§€ ì „ ì»¤ë°‹)
                Debug.Log("½ÇÆÐ");
                return;
            }
        }
<<<<<<< HEAD
=======

        // Á¶°Ç ÀüºÎ Åë°úÇÔ
>>>>>>> parent of 8b20a26 (0321 ë¨¸ì§€ ì „ ì»¤ë°‹)
        ClearStage();
    }

    public void ClearStage()
    {
        // SO¿¡ Å¬¸®¾î Ã³¸®
        CurTsumegoInfo.IsClear = true;
        Debug.Log("¼º°ø");

        // Å¬¸®¾î ¿¬Ãâ, º¸»ó Áö±Þ, Å¬¸®¾î µ¥ÀÌÅÍ °»½Å Ã³¸®
    }
}
