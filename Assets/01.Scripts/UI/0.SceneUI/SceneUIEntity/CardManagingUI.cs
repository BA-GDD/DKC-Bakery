using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardManagingUI : SceneUI
{
    [SerializeField] private int _loadStoneCount;
    public int LoadStoneCount => _loadStoneCount;

    public CardShameElementSO CurrentCardShameElementInfo { get; set; }

    [SerializeField] private UnityEvent<float> _onPressLevelUpEvent;

    public void PressLevelUpButton()
    {
        int toUseGoods = CurrentCardShameElementInfo.cardLevel * 50;

        if(CanUseGoods(toUseGoods))
        {
            _onPressLevelUpEvent?.Invoke(toUseGoods * 0.4f);
        }
    }

    public bool CanUseGoods(int toUseGoods)
    {
        return LoadStoneCount >= toUseGoods;
    }
}
