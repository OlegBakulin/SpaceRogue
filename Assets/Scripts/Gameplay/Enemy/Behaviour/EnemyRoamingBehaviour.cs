using Gameplay.Enemy.Movement;
using Gameplay.Player;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyRoamingBehaviour : EnemyBehaviour
    {
        private readonly EnemyMovementModel _movementModel;
        
        public EnemyRoamingBehaviour(SubscribedProperty<EnemyState> enemyState, PlayerView playerView, EnemyMovementModel movementModel) : base(enemyState, playerView)
        {
            _movementModel = movementModel;
        }
        
        protected override void OnUpdate()
        {
            //TODO move view randomly
        }
    }
}