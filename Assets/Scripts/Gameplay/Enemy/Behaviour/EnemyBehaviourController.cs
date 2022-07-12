using Abstracts;
using Gameplay.Enemy.Movement;
using Gameplay.Player;
using Gameplay.Shooting;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyBehaviourController : BaseController
    {
        private readonly SubscribedProperty<EnemyState> _enemyCurrentState;
        private readonly EnemyBehaviourMovementModel _movementModel;
        private readonly FrontalTurretController _turretController;
        private readonly PlayerView _playerView;
        
        private EnemyBehaviour _currentBehaviour;

        public EnemyBehaviourController(EnemyMovementModel movementModel, EnemyView view, FrontalTurretController turretController, PlayerView playerView)
        {
            _movementModel = new EnemyBehaviourMovementModel(movementModel, view, playerView);
            _turretController = turretController;
            _playerView = playerView;

            _enemyCurrentState = new SubscribedProperty<EnemyState>(EnemyState.PassiveRoaming);
            _currentBehaviour = new EnemyIdleBehaviour(_enemyCurrentState, playerView);
            _enemyCurrentState.Subscribe(OnEnemyStateChange);
            OnEnemyStateChange(EnemyState.PassiveRoaming);
        }

        protected override void OnDispose()
        {
            _enemyCurrentState.Unsubscribe(OnEnemyStateChange);
        }
        
        private void OnEnemyStateChange(EnemyState newState)
        {
            _currentBehaviour.Dispose();
            
            switch (newState)
            {
                case EnemyState.Idle:
                    _currentBehaviour = new EnemyIdleBehaviour(_enemyCurrentState, _playerView);
                    break;
                case EnemyState.PassiveRoaming:
                    _currentBehaviour = new EnemyRoamingBehaviour(_enemyCurrentState, _playerView, _movementModel);
                    break;
                case EnemyState.InCombat:
                    _currentBehaviour = new EnemyCombatBehaviour(_enemyCurrentState, _playerView, _movementModel, _turretController);
                    break;
                default: return;
            }
        }
    }
}