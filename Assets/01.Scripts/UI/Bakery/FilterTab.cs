using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterTab : MonoBehaviour
{
    [SerializeField] private Button _tapBtn;
    public Button TapBtn { get; set; }
    [SerializeField] private IngredientType _ingredientType;
    public IngredientType GetIngredientType => _ingredientType;
}
