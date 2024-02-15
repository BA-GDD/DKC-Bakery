using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElbumFactory : MonoBehaviour
{
    [SerializeField] private GameObject _noneElbum;
    private readonly string _pictureDataKey = "PictureDataKEY";

    public void ElbumGenerate(int phase)
    {
        if(DataManager.Instance.IsHaveData(_pictureDataKey))
        {
            _noneElbum.gameObject.SetActive(false);

            List<Sprite> currentSpriteList = 
            DataManager.Instance.LoadData<PictureData>(_pictureDataKey).picutureSpriteList;

            int startIdx = (phase - 1) * 10;
            int endIdx = Mathf.Min(10, currentSpriteList.Count - startIdx);
            List<Sprite> phaseRangeList = currentSpriteList.GetRange(startIdx, endIdx);

            foreach(Sprite sp in phaseRangeList)
            {
                CakeElbum ce = PoolManager.Instance.Pop(PoolingType.CakeElbum) as CakeElbum;
                ce.SetUp(sp, Random.Range(-3, 3));

                ce.transform.SetParent(transform);
            }
        }
        else
        {
            _noneElbum.gameObject.SetActive(true);
        }
    }
}
