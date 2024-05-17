
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionFunction
{
    public static class TransformExtension
    {
        private static Dictionary<Transform, Vector2> _onPlayingPosTweenDic = new();
        private static Dictionary<Transform, Quaternion> _onPlayingRotTweenDic = new();

        public static void Clear(this Transform trm)
        {
            foreach(Transform child in trm)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        private static void GeneratePosition(Transform trm, Vector2 targetPos, bool isLocal)
        {
            if (_onPlayingPosTweenDic.ContainsKey(trm))
            {
                trm.DOKill();

                if (isLocal)
                {
                    trm.localPosition = _onPlayingPosTweenDic[trm];
                }
                else
                {
                    trm.position = _onPlayingPosTweenDic[trm];
                }
            }
            else
            {
                _onPlayingPosTweenDic.Add(trm, targetPos);
            }
        }
        private static void GenerateRotation(Transform trm, Quaternion targetRot, bool isLocal)
        {
            if (_onPlayingRotTweenDic.ContainsKey(trm))
            {
                trm.DOKill();

                if (isLocal)
                {
                    trm.localRotation = _onPlayingRotTweenDic[trm];
                }
                else
                {
                    trm.rotation = _onPlayingRotTweenDic[trm];
                }
            }
            else
            {
                _onPlayingRotTweenDic.Add(trm, targetRot);
            }
        }

        public static void SmartMove(this Transform trm, bool isLocal, Vector2 targetPos, float time, Ease easing = Ease.Linear)
        {
            GeneratePosition(trm, targetPos, isLocal);

            if(isLocal)
            {
                trm.DOLocalMove(targetPos, time).SetEase(easing);
            }
            else
            {
                trm.DOMove(targetPos, time).SetEase(easing);
            }
        }
        public static void SmartMoveX(this Transform trm, bool isLocal, float targetX, float time, Ease easing = Ease.Linear)
        {
            if (isLocal)
            {
                GeneratePosition(trm, new Vector2(targetX, trm.localPosition.y), isLocal);
                trm.DOLocalMoveX(targetX, time).SetEase(easing);
            }
            else
            {
                GeneratePosition(trm, new Vector2(targetX, trm.position.y), isLocal);
                trm.DOMoveX(targetX, time).SetEase(easing);
            }
        }
        public static void SmartMoveY(this Transform trm, bool isLocal, float targetY, float time, Ease easing = Ease.Linear)
        {
            if (isLocal)
            {
                GeneratePosition(trm, new Vector2(trm.localPosition.x, targetY), isLocal);
                trm.DOLocalMoveY(targetY, time).SetEase(easing);
            }
            else
            {
                GeneratePosition(trm, new Vector2(trm.position.x, targetY), isLocal);
                trm.DOMoveY(targetY, time).SetEase(easing);
            }
        }
        public static void SmartRotation(this Transform trm, bool isLocal, Quaternion targetRot, float time, Ease easing = Ease.Linear)
        {
            if (isLocal)
            {
                GenerateRotation(trm, targetRot, isLocal);
                trm.DOLocalRotateQuaternion(targetRot, time).SetEase(easing);
            }
            else
            {
                GenerateRotation(trm, targetRot, isLocal);
                trm.DORotateQuaternion(targetRot, time).SetEase(easing);
            }
        }
    }
}
