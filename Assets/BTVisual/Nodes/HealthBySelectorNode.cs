using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{

    public class HealthBySelectorNode : CompositeNode
    {
        public enum CompareHealthInfo { less, greater }
        private Node _beforeNode;

        [System.Serializable]
        public struct HealthSelectorInfo
        {
            [Range(0f, 1f)] public float healthAmount;
            public CompareHealthInfo compareType;
        }

        public List<HealthSelectorInfo> healthSelectors;

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            bool canPatten = false;

            float curHealth = enemy.HealthCompo.GetNormalizedHealth();
            if (_beforeNode != null)
            {
                State state = _beforeNode.Update();
                if (state != State.RUNNING)
                    _beforeNode = null;
                return state;
            }

            for (int i = 0; i < healthSelectors.Count; i++)
            {
                Node child = null;
                child = children[i];
                HealthSelectorInfo info = healthSelectors[i];
                switch (info.compareType)
                {
                    case CompareHealthInfo.less:
                        canPatten = curHealth < info.healthAmount;
                        break;
                    case CompareHealthInfo.greater:
                        canPatten = curHealth > info.healthAmount;
                        break;
                }
                if (canPatten)
                {
                    _beforeNode = child;
                    State state = child.Update();
                    if (state != State.FAILURE)
                    {
                        return state;
                    }
                }
            }
            _beforeNode = null;
            return State.FAILURE;
        }
    }
}

