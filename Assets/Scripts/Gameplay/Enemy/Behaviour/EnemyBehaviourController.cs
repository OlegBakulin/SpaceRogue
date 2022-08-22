using Abstracts;
using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Player;
using Gameplay.Shooting;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyBehaviourController : BaseController
    {
        private readonly SubscribedProperty<EnemyState> _enemyCurrentState;
        private readonly EnemyInputController _inputController;
        private readonly EnemyMovementController _movementController;
        private readonly MovementModel _movementModel;
        private readonly FrontalTurretController _turretController;
        private readonly EnemyView _view;
        private readonly PlayerView _playerView;
        
        private EnemyBehaviour _currentBehaviour;

        public EnemyBehaviourController(MovementModel movementModel, EnemyView view, FrontalTurretController turretController, PlayerView playerView)
        {
            _view = view;
            _movementModel = movementModel;
            _inputController = AddInputController();
            _movementController = AddMovementController();

            _turretController = turretController;
            _playerView = playerView;

            _enemyCurrentState = new SubscribedProperty<EnemyState>(EnemyState.PassiveRoaming);
            _enemyCurrentState.Subscribe(OnEnemyStateChange);
            OnEnemyStateChange(EnemyState.PassiveRoaming);
        }

        protected override void OnDispose()
        {
            _enemyCurrentState.Unsubscribe(OnEnemyStateChange);
            _currentBehaviour.Dispose();
        }
        
        private void OnEnemyStateChange(EnemyState newState)
        {
            _currentBehaviour?.Dispose();
            
            switch (newState)
            {
                case EnemyState.Idle:
                    _currentBehaviour = new EnemyIdleBehaviour(_enemyCurrentState, _view, _playerView);
                    break;
                case EnemyState.PassiveRoaming:
                    _currentBehaviour = new EnemyRoamingBehaviour(_enemyCurrentState, _view, _playerView, _movementModel, _inputController);
                    break;
                case EnemyState.InCombat:
                    _currentBehaviour = new EnemyCombatBehaviour(_enemyCurrentState, _view, _playerView, _movementModel, _inputController, _turretController);
                    break;
                default: return;
            }
        }

        private EnemyInputController AddInputController()
        {
            var inputController = new EnemyInputController();
            AddController(inputController);
            return inputController;
        }

        private EnemyMovementController AddMovementController()
        {
            var (horizontalInput, verticalInput) = _inputController;
            var movementController = new EnemyMovementController(horizontalInput, verticalInput, _movementModel, _view);
            AddController(movementController);
            return movementController;
        }
    }
}