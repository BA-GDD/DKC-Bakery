using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;
using UnityEngine.UI;
using TMPro;

public class BakeryUI : SceneUI
{
    [SerializeField] private Transform _ingredientTrm;
    public Transform IngredientTrm => _ingredientTrm;

    private RecipePanel _recipePanel;
    public RecipePanel RecipePanel
    {
        get
        {
            if(_recipePanel == null)
            {
                _recipePanel = FindObjectOfType<RecipePanel>();
            }
            return _recipePanel;
        }
    }
}
