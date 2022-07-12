using System.Collections.Generic;
using Abstracts;
using Gameplay.Shooting;
using Scriptables.Modules;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;


namespace Gameplay.Player.FrontalGuns
{
    public class FrontalGunsController : BaseController
    {
        private readonly SubscribedProperty<bool> _primaryFireInput;
        private readonly List<TurretModuleConfig> _turretConfigs;
        private readonly List<FrontalTurretController> _turretControllers;

        public FrontalGunsController(SubscribedProperty<bool> primaryFireInput, List<TurretModuleConfig> turretConfigs, PlayerView playerView)
        {
            _primaryFireInput = primaryFireInput;
            _turretConfigs = turretConfigs;
            
            _turretControllers = new List<FrontalTurretController>();

            foreach (var config in turretConfigs)
            {
                InitializeTurret(config, playerView.transform);
            }
            
            _primaryFireInput.Subscribe(HandleFiring);
        }

        protected override void OnDispose()
        {
            _primaryFireInput.Unsubscribe(HandleFiring);
        }

        private void HandleFiring(bool isFiring)
        {
            foreach (var turret in _turretControllers)
            {
                if (isFiring) turret.CommenceFiring();
            }
        }

        private void InitializeTurret(TurretModuleConfig turretConfig, Transform transform)
        {
            var turretController = WeaponFactory.CreateFrontalTurret(turretConfig, transform);
            AddController(turretController);
            _turretControllers.Add(turretController);
        }
    }
}