using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyIdleBehaviour : EnemyBehaviour
    {
        public EnemyIdleBehaviour(SubscribedProperty<EnemyState> enemyState, EnemyView view, PlayerView playerView) : base(enemyState, view, playerView)
        {
        }

        protected override void OnUpdate()
        {
            //TODO Stay idle until target found (or forever)
        }
    }
}