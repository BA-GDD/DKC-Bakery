using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsType
{

}

[CreateAssetMenu(menuName = "SO/Items/GoodsItem")]
public class GoodsItemSO : ItemDataSO
{
    public GoodsType goodsType;
}
