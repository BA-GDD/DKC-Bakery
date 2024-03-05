using System.Linq;

namespace BTVisual
{
    public abstract class FlontrolPattrenNode : ActionNode
    {
        protected new Flontrol enemy;
        public string parameterName;

        protected EnemyDangerAttackArea attackArea;

        protected override void OnStart()
        {
            enemy.AnimatorCompo.SetBool(parameterName, true);
            attackArea = this.enemy.pattenAreaInfo.First(a => a.pattenName == parameterName).areas;
        }
        protected override void OnStop()
        {
            enemy.endAnimationTrigger = false;
            enemy.AnimatorCompo.SetBool(parameterName, false);
            attackArea.Reset();
        }

        public override void Bind(BlackBoard blackboard, Context context, Enemy enemy)
        {
            base.Bind(blackboard, context, enemy);
            this.enemy = (Flontrol)enemy;
        }
    }
}