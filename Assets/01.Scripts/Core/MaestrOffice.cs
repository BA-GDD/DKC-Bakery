using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MaestrOffice
{
    private static Camera _cam;
    public static Camera Camera
    {
        get
        {
            if(_cam != null)
            {
                return _cam;
            }

            _cam = Camera.main;

            return _cam;
        }
    }

    public static int GetPlusOrMinus()
    {
        return (int)Mathf.Sign(UnityEngine.Random.Range(0, 2) * 2 - 1);
    }

    public static Vector2 GetWorldPosToScreenPos(Vector2 screenPos)
    {
        return Camera.ScreenToWorldPoint(screenPos);
    }

    public static Vector3 GetWorldPosToScreenPos(Vector3 screenPos)
    {
        return Camera.ScreenToWorldPoint(screenPos);
    }

    public static Vector2 GetScreenPosToWorldPos(Vector2 worldPos)
    {
        Vector3 screenPos = Camera.WorldToScreenPoint(worldPos);
        
        return screenPos;
    }

    public static int BoolToInt(bool value)
    {
        return Convert.ToInt16(value);
    }

    public static int SelectedNumberOfFlagEnums(int flagEnum)
    {
        int count = 0;
        while (flagEnum > 0)
        {
            count += flagEnum & 1;
            flagEnum >>= 1;
        }
        return count;
    }
}
