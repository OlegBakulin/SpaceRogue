using Scriptables.Modules;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalLaserController : FrontalTurretController
    {
        private readonly LaserWeaponConfig _weaponConfig;
        private LineRenderer _line;
        private Transform _playerTransform;
        public FrontalLaserController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var laserConfig = config.SpecificWeapon as LaserWeaponConfig;
            _weaponConfig = laserConfig
                ? laserConfig
                : throw new System.Exception("Wrong config type was provided");
            _playerTransform = gunPointParentTransform;
        }

        public override void CommenceFiring()
        {
            if (IsOnCooldown)
            {
                return;
            } 

            FireLaserProjectiles(_weaponConfig.MaxActiveTime_M, _weaponConfig.Long_Dlina);
            CooldownTimer.Start();
        }

        private void FireLaserProjectiles(float activeTime, float liserLong)
        {
            _line = _playerTransform.GetComponent<LineRenderer>();
            Time timerDamage = new Time();
            timerDamage = activeTime * _weaponConfig.Koefficient_X;
            var projectile = ProjectileFactory.CreateProjectile();
            RaycastHit hit;
            if (Physics.Raycast(_playerTransform.position, Vector3.up, out hit, 10000f))
            {
                _line.SetPosition(1, hit.point);
            }

            AddController(projectile);
        }
    }
}
