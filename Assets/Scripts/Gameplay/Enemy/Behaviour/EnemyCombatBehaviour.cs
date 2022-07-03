using Gameplay.Enemy.Movement;
using Gameplay.Player;
using Gameplay.Shooting;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyCombatBehaviour : EnemyBehaviour
    {
        private readonly EnemyBehaviourMovementModel _movementModel;
        private readonly FrontalTurretController _frontalTurret;
        private readonly PlayerView _target;
        
        public EnemyCombatBehaviour(SubscribedProperty<EnemyState> enemyState, PlayerView playerView, EnemyBehaviourMovementModel movementModel, FrontalTurretController frontalTurret) : base(enemyState, playerView)
        {
            _movementModel = movementModel;
            _frontalTurret = frontalTurret;
        }

        protected override void OnUpdate()
        {
            //TODO implement hit-and-run tactics
        }
    }
}