using Abstracts;
using Gameplay.Enemy.Behaviour;
using Gameplay.Shooting;
using Scriptables.Enemy;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy
{
    public class EnemyController : BaseController
    {
        private readonly SubscribedProperty<EnemyState> _enemyCurrentState;
        private readonly EnemyView _view;
        private readonly EnemyConfig _config;
        private readonly FrontalTurretController _turret;
        
        private EnemyBehaviour _currentBehaviour;

        public EnemyController(EnemyConfig config, EnemyView view)
        {
            _config = config;
            _view = view;
            AddGameObject(_view.gameObject);
            _turret = new FrontalTurretController(_config.Weapon, _view.transform);
            AddController(_turret);

            _enemyCurrentState = new SubscribedProperty<EnemyState>(EnemyState.PassiveRoaming);
            _currentBehaviour = new EnemyIdleBehaviour(_enemyCurrentState);
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
                    _currentBehaviour = new EnemyIdleBehaviour(_enemyCurrentState);
                    break;
                case EnemyState.PassiveRoaming:
                    _currentBehaviour = new EnemyRoamingBehaviour(_enemyCurrentState, _view);
                    break;
                case EnemyState.SeekingPlayer:
                    _currentBehaviour = new EnemySeekingPlayerBehaviour(_enemyCurrentState, _view);
                    break;
                case EnemyState.InCombat:
                    _currentBehaviour = new EnemyCombatBehaviour(_enemyCurrentState, _view, _turret);
                    break;
                default: return;
            }
        }
    }
}