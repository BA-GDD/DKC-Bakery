using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BakeryFilterTab : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RecipeSortType _sortType;
    private RecipePanel _recipePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        GetRecipePanel().InvokeRecipeAction(_sortType);
    }

    protected RecipePanel GetRecipePanel()
    {
        if(_recipePanel == null )
        {
            _recipePanel = UIManager.Instance.GetSceneUI<BakeryUI>().RecipePanel;
        }

        return _recipePanel;
    }
}
