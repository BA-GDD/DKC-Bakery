using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UIDefine;

public class CookingBox : MonoBehaviour
{
    private IngredientType _selectIngredientType = IngredientType.None;
    private Dictionary<IngredientType, SelectIngredientBox> _getBoxInstanceDic = new Dictionary<IngredientType, SelectIngredientBox>();

    [SerializeField] private Transform _blurParent;
    private BlurObject[] _blurObjectArr;

    private void Awake()
    {
        SelectIngredientBox[] boxs = GetComponentsInChildren<SelectIngredientBox>();
        foreach(SelectIngredientBox box in boxs)
        {
            if(_getBoxInstanceDic.ContainsKey(box.IngredientType))
            {
                Debug.LogError($"Error!! {box.IngredientType} is Already Contains.. plz Check");
                continue;
            }

            _getBoxInstanceDic.Add(box.IngredientType, box);
        }
        _blurObjectArr = _blurParent.GetComponentsInChildren<BlurObject>();
    }

    public void AddSelectIngredientInfo(ItemDataIngredientSO ingInfo)
    {
        _getBoxInstanceDic[ingInfo.ingredientType].SelectIngredient(ingInfo);

        if(_selectIngredientType.HasFlag(IngredientType.None))
        {
            _selectIngredientType &= ~IngredientType.None;
        }

        _selectIngredientType |= ingInfo.ingredientType;

        if(MaestrOffice.SelectedNumberOfFlagEnums((int)_selectIngredientType) == 5)
        {
            ChangeBlurObject(BakeryCombinationType.cake);
        }
        else
        {
            ChangeBlurObject(BakeryCombinationType.dough);
        }
    }

    public void RemoveSelectIngredientInfo(IngredientType ingType)
    {
        _getBoxInstanceDic[ingType].RemoveIngredient();

        _selectIngredientType &= ~ingType;

        if(Enum.IsDefined(typeof(IngredientType), _selectIngredientType))
        {
            _selectIngredientType |= IngredientType.None;
            ChangeBlurObject(BakeryCombinationType.none);
        }
    }

    private void ChangeBlurObject(BakeryCombinationType type)
    {
        foreach(BlurObject blurObj in _blurObjectArr)
        {
            blurObj.gameObject.SetActive(blurObj.CombinationType == type);
        }
    }

}