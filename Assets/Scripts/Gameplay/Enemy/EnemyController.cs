using Abstracts;
using Gameplay.Enemy.Behaviour;
using Gameplay.Enemy.Movement;
using Gameplay.Health;
using Gameplay.Movement;
using Gameplay.Player;
using Gameplay.Shooting;
using Scriptables.Enemy;
using Scriptables.Health;

namespace Gameplay.Enemy
{
    public class EnemyController : BaseController
    {
        private readonly EnemyView _view;
        private readonly EnemyConfig _config;
        private readonly FrontalTurretController _turret;
        private readonly EnemyMovementController _movementController;
        private readonly EnemyBehaviourController _behaviourController;
        private readonly HealthController _healthController;
        private readonly PlayerController _playerController;

        public EnemyController(EnemyConfig config, EnemyView view, PlayerController playerController)
        {
            _playerController = playerController;
            _config = config;
            _view = view;
            AddGameObject(_view.gameObject);
            _turret = WeaponFactory.CreateFrontalTurret(_config.Weapon, _view.transform);
            AddController(_turret);
            
            var movementModel = new MovementModel(_config.Movement);
            _behaviourController = new EnemyBehaviourController(movementModel, _view, _turret, _playerController, _config.Behaviour);
            AddController(_behaviourController);

            _healthController = AddHealthController(_config.Health, _config.Shield);
        }

        private HealthController AddHealthController(HealthConfig healthConfig, ShieldConfig shieldConfig)
        {
            var healthController = shieldConfig is null
                ? new HealthController(healthConfig, _view)
                : new HealthController(healthConfig, shieldConfig, _view);
            
            healthController.SubscribeToOnDestroy(Dispose);
            AddController(_healthController);
            return healthController;
        }
    }
}