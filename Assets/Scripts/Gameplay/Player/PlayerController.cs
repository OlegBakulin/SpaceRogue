using System.Collections.Generic;
using Abstracts;
using Gameplay.Health;
using Gameplay.Input;
using Gameplay.Player.FrontalGuns;
using Gameplay.Player.Inventory;
using Gameplay.Player.Movement;
using Scriptables;
using Scriptables.Modules;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.ResourceManagement;

namespace Gameplay.Player
{
    public class PlayerController : BaseController
    {
        public PlayerView View => _view;
        public HealthModel HealthModel => _healthShieldModel;

        private readonly ResourcePath _configPath = new("Configs/PlayerConfig");
        private readonly ResourcePath _viewPath = new("Prefabs/Gameplay/Player");
        private readonly ResourcePath _healthPath = new ResourcePath("Configs/HealthConfig");
        private readonly ResourcePath _shieldPath = new ResourcePath("Configs/BaseShieldConfig");

        private readonly PlayerConfig _config;
        private readonly PlayerView _view;
        private readonly HealthModel _healthShieldModel;

        private readonly HealthConfig _healthConfig;
        private readonly ShieldModuleConfig _shieldModuleConfig;

        private readonly SubscribedProperty<float> _horizontalInput = new();
        private readonly SubscribedProperty<float> _verticalInput = new();
        private readonly SubscribedProperty<bool> _primaryFireInput = new();

        public PlayerController()
        {
            _config = ResourceLoader.LoadObject<PlayerConfig>(_configPath);
            _view = LoadView<PlayerView>(_viewPath, Vector3.zero);
            _healthConfig = ResourceLoader.LoadObject<HealthConfig>(_healthPath);
            _shieldModuleConfig = ResourceLoader.LoadObject<ShieldModuleConfig>(_shieldPath);
            _healthShieldModel = new HealthModel(_healthConfig, _shieldModuleConfig);

            var inputController = new InputController(_horizontalInput, _verticalInput, _primaryFireInput);
            AddController(inputController);

            var inventoryController = AddInventoryController(_config.Inventory);
            var movementController = AddMovementController(inventoryController.Engine, _view);
            var frontalGunsController = AddFrontalGunsController(inventoryController.Turrets, _view);
            var healthController = AddHealthController(_healthConfig, _shieldModuleConfig);
        }
        
        private HealthController AddHealthController(HealthConfig healthConfig, ShieldModuleConfig shieldConfig)
        {
            var healthController = new HealthController(healthConfig, shieldConfig);
            AddController(healthController);
            return healthController;
        }

        private PlayerInventoryController AddInventoryController(PlayerInventoryConfig config)
        {
            var inventoryController = new PlayerInventoryController(_config.Inventory);
            AddController(inventoryController);
            return inventoryController;
        }

        private PlayerMovementController AddMovementController(EngineModuleConfig movementConfig, PlayerView view)
        {
            var movementController = new PlayerMovementController(_horizontalInput, _verticalInput, movementConfig, view);
            AddController(movementController);
            return movementController;
        }

        private FrontalGunsController AddFrontalGunsController(List<TurretModuleConfig> turretConfigs, PlayerView view)
        {
            var frontalGunsController = new FrontalGunsController(_primaryFireInput, turretConfigs, view);
            AddController(frontalGunsController);
            return frontalGunsController;
        }
    }
}