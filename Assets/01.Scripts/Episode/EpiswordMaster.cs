using EpisodeDialogueDefine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EpiswordMaster
{
    public static CharacterType GetCharacterTypeByName(string name)
    {
        switch (name)
        {
            case "Ÿ��Ʈ":
                return CharacterType.tart;
            case "???":
                return CharacterType.mawang;
            case "ũ��":
                return CharacterType.mawang;
            case "ũ���� �����":
                return CharacterType.mawang;
            default:
                return CharacterType.tart;
        }
    }

    public static Vector2 GetEmotionReactionPos(Vector2 characterPos)
    {
        Vector2 calPos = Vector2.zero;
        if(characterPos.x >= 0)
        {
            calPos = characterPos + new Vector2(-200, 442);
        }
        else
        {
            calPos = characterPos += new Vector2(200, 442);
        }

        return calPos;
    }
}
