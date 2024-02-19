namespace BTVisual
{
    public abstract class FlontrolPattrenNode : ActionNode
    {
        protected new Flontrol enemy;
        public string parameterName;

        protected override void OnStart()
        {
            enemy.AnimatorCompo.SetBool(parameterName, true);
        }
        protected override void OnStop()
        {
            enemy.endAnimationTrigger = false;
            enemy.AnimatorCompo.SetBool(parameterName, false);
        }

        public override void Bind(BlackBoard blackboard, Context context, Enemy enemy)
        {
            base.Bind(blackboard, context, enemy);
            this.enemy = (Flontrol)enemy;
        }
    }
}