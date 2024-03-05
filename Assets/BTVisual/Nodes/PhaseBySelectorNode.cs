using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class PhaseBySelectorNode : CompositeNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
            foreach (var child in children)
            {
                if (child.started)
                    child.Break();
            }
        }

        protected override State OnUpdate()
        {
            return children[enemy.phase].Update();
        }
    }
}
