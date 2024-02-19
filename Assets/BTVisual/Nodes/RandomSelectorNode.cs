using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class RandomSelectorNode : CompositeNode
    {
        private int _ranNum;
        protected override void OnStart()
        {
            _ranNum = Random.Range(0, children.Count);
        }

        protected override void OnStop()
        {
            foreach (var child in children)
            {
                if(child.started)
                    child.Break();
            }
        }

        protected override State OnUpdate()
        {
            return children[_ranNum].Update();
        }
    }
}