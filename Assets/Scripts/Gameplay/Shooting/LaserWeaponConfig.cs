using System;
using UnityEngine;

namespace Gameplay.Shooting
{
    [CreateAssetMenu(fileName = nameof(LaserWeaponConfig), menuName = "Configs/Weapons/" + nameof(LaserWeaponConfig))]
    public class LaserWeaponConfig : SpecificWeaponConfig
    {
        [field: SerializeField] public ProjectileConfig Projectile { get; private set; }
        [field: SerializeField] public LineRenderer LaserLineRender { get; set;}
        [field: SerializeField, Min(0.1f)] public float LaserLong { get; private set; } = 1f;
        [field: SerializeField, Min(0.0001f)] public float MaxLaserActiveTime { get; private set; } = 0.0001f;
        [field: SerializeField, Range(0.0001f, 1f)] public float KoefficientTimeColldown { get; private set; } = 1f;
    }
}
