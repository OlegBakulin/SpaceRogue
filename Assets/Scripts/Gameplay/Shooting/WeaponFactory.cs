using System;
using Scriptables.Modules;
using UnityEngine;

namespace Gameplay.Shooting
{
    public static class WeaponFactory
    {
        public static FrontalTurretController CreateFrontalTurret(TurretModuleConfig config, Transform gunPointParentTransform)
        {
            return config.WeaponType switch
            {
                WeaponType.None => new FrontalNullGunController(config, gunPointParentTransform),
                WeaponType.Blaster => new FrontalBlasterController(config, gunPointParentTransform),
                WeaponType.Shotgun =>
                    //TODO implement
                    new FrontalNullGunController(config, gunPointParentTransform),
                WeaponType.Minigun =>
                    //TODO implement
                    new FrontalNullGunController(config, gunPointParentTransform),
                WeaponType.Railgun =>
                    //TODO implement
                    new FrontalNullGunController(config, gunPointParentTransform),
                _ => throw new ArgumentOutOfRangeException(nameof(config.WeaponType), config.WeaponType, "A not-existent weapon type is provided")
            };
        }
    }
}