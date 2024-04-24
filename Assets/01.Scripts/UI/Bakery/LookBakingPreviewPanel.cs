using UnityEngine;
using UnityEngine.UI;

public class LookBakingPreviewPanel : PreviewPanel
{
    [SerializeField] 
    private Image[] _ingredientElements = new Image[3];

    [SerializeField] private GameObject _cakeImgObj;
    [SerializeField] private GameObject _questionMarkObj;

    protected override void LookUpContent()
    {
        foreach(var element in _ingredientElements)
        {
            element.enabled = false;
        }
    }

    public void SetIngredientElement(ItemDataIngredientSO ingData)
    {
        var element = _ingredientElements[(int)ingData.ingredientType];

        element.enabled = true;
        element.sprite = ingData.itemIcon;
    }
}
