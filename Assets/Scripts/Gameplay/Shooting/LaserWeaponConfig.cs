using System;
using UnityEngine;

namespace Gameplay.Shooting
{
    [CreateAssetMenu(fileName = nameof(LaserWeaponConfig), menuName = "Configs/Weapons/" + nameof(LaserWeaponConfig))]
    public class LaserWeaponConfig : SpecificWeaponConfig
    {
        [field: SerializeField] public ProjectileConfig Projectile { get; private set; }
        [field: SerializeField] public LineRenderer LaserObject { get; set;}
        [field: SerializeField, Min(0.1f)] public float Long_Dlina { get; private set; } = 1f;
        [field: SerializeField, Min(0.0001f)] public float MaxActiveTime_M { get; private set; } = 0.0001f;
        [field: SerializeField, Range(0f, 1f)] public float Koefficient_X { get; private set; } = 1f;
    }
}
