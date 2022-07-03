using Gameplay.Enemy.Movement;
using Gameplay.Player;
using Gameplay.Shooting;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyCombatBehaviour : EnemyBehaviour
    {
        private readonly EnemyMovementController _movementController;
        private readonly FrontalTurretController _frontalTurret;
        private readonly PlayerView _target;
        
        public EnemyCombatBehaviour(SubscribedProperty<EnemyState> enemyState, PlayerView playerView, EnemyMovementController movementController, FrontalTurretController frontalTurret) : base(enemyState, playerView)
        {
            _movementController = movementController;
            _frontalTurret = frontalTurret;
        }

        protected override void OnUpdate()
        {
            //TODO implement hit-and-run tactics
        }
    }
}