using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyIdleBehaviour : EnemyBehaviour
    {
        public EnemyIdleBehaviour(SubscribedProperty<EnemyState> enemyState, PlayerView playerView) : base(enemyState, playerView)
        {
        }

        protected override void OnUpdate()
        {
            //TODO Stay idle until target found (or forever)
        }
    }
}