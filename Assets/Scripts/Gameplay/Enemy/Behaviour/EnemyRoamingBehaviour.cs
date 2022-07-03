using Gameplay.Enemy.Movement;
using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyRoamingBehaviour : EnemyBehaviour
    {
        private readonly EnemyMovementController _movementController;
        
        public EnemyRoamingBehaviour(SubscribedProperty<EnemyState> enemyState, PlayerView playerView, EnemyMovementController movementController) : base(enemyState, playerView)
        {
            _movementController = movementController;
        }
        
        protected override void OnUpdate()
        {
            //TODO move view randomly
        }
    }
}