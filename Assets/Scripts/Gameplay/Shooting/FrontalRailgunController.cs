using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class FrontalRailgunController : FrontalTurretController
    {
        private readonly RailgunWeaponConfig _weaponConfig;

        public FrontalRailgunController(TurretModuleConfig config, Transform gunPointParentTransform) : base(config, gunPointParentTransform)
        {
            var railgunConfig = config.SpecificWeapon as RailgunWeaponConfig;
            _weaponConfig = railgunConfig 
                ? railgunConfig 
                : throw new System.Exception("wrong config type was provided");
        }

        public override void CommenceFiring()
        {
            if (IsOnCooldown)
            {
                return;
            }

            var projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);

            CooldownTimer.Start();
        }
    }   
}
