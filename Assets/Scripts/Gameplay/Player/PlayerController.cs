using System;
using System.Collections.Generic;
using Abstracts;
using Gameplay.Health;
using Gameplay.Input;
using Gameplay.Movement;
using Gameplay.Player.FrontalGuns;
using Gameplay.Player.Inventory;
using Gameplay.Player.Movement;
using Scriptables;
using Scriptables.Health;
using Scriptables.Modules;
using UI.Game;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.ResourceManagement;
using Utilities.Unity;

namespace Gameplay.Player
{
    public class PlayerController : BaseController
    {
        public PlayerView View => _view;

        private readonly ResourcePath _configPath = new("Configs/Player/PlayerConfig");
        private readonly ResourcePath _viewPath = new("Prefabs/Gameplay/Player");

        private readonly PlayerConfig _config;
        private readonly PlayerView _view;

        private readonly SubscribedProperty<float> _horizontalInput = new();
        private readonly SubscribedProperty<float> _verticalInput = new();
        private readonly SubscribedProperty<bool> _primaryFireInput = new();

        public event Action PlayerDestroyed = () => { };

        public PlayerController()
        {
            _config = ResourceLoader.LoadObject<PlayerConfig>(_configPath);
            _view = LoadView<PlayerView>(_viewPath, StartedPlayerPosition());

            var inputController = new InputController(_horizontalInput, _verticalInput, _primaryFireInput);
            AddController(inputController);

            var inventoryController = AddInventoryController(_config.Inventory);
            var movementController = AddMovementController(_config.Movement, _view);
            var frontalGunsController = AddFrontalGunsController(inventoryController.Turrets, _view);
            var healthController = AddHealthController(_config.HealthConfig, _config.ShieldConfig);
        }

        private HealthController AddHealthController(HealthConfig healthConfig, ShieldConfig shieldConfig)
        {
            var healthController = new HealthController(healthConfig, shieldConfig, GameUIController.PlayerStatusBarView, _view);
            healthController.SubscribeToOnDestroy(Dispose);
            healthController.SubscribeToOnDestroy(OnPlayerDestroyed);
            AddController(healthController);
            return healthController;
        }

        private PlayerInventoryController AddInventoryController(PlayerInventoryConfig config)
        {
            var inventoryController = new PlayerInventoryController(_config.Inventory);
            AddController(inventoryController);
            return inventoryController;
        }

        private PlayerMovementController AddMovementController(MovementConfig movementConfig, PlayerView view)
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

        private Vector3 StartedPlayerPosition()
        {
            Vector3 startPlayerPosition = RandomizePositionAtMinus400ToPlus400();
            while (UnityHelper.IsAnyObjectAtPosition(startPlayerPosition, 40f))
            {
                startPlayerPosition = RandomizePositionAtMinus400ToPlus400();
            };
            return startPlayerPosition;
        }

        private Vector3 RandomizePositionAtMinus400ToPlus400()
        {
            System.Random random = new System.Random();
            return new Vector3(random.Next(-400, 400), random.Next(-400, 400), 0);
        }

        public void OnPlayerDestroyed() 
        {
            PlayerDestroyed();
        }
    }
}