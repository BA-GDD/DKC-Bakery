using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisualSetter : CardSetter
{
    [SerializeField] private TextMeshProUGUI _cardCostText;
    [SerializeField] private TextMeshProUGUI _cardNameText;
    [SerializeField] private Image _cardVisual;

    public override void SetCardInfo(CardShameElementSO shameData, CardInfo cardInfo, int combineLevel)
    {
        _cardCostText.text = cardInfo.AbillityCost.ToString();
        _cardNameText.text = cardInfo.CardName;
        _cardVisual.sprite = cardInfo.CardVisual;
    }
}
