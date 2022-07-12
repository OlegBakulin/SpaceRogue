using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyIdleBehaviour : EnemyBehaviour
    {
        public EnemyIdleBehaviour(SubscribedProperty<EnemyState> enemyState) : base(enemyState)
        {
        }

        protected override void OnUpdate()
        {
            //TODO Stay idle until target found (or forever)
        }
    }
}