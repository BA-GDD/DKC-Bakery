using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class AlreadyUsedNode : ActionNode
    {
        private bool _isAlreadyActive;
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
            _isAlreadyActive = true;
        }

        protected override State OnUpdate()
        {
            return _isAlreadyActive ? State.FAILURE : State.SUCCESS;
        }
    }
}