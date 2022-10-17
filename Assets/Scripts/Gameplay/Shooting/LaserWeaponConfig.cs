using UnityEngine;

namespace Gameplay.Shooting
{
    [CreateAssetMenu(fileName = nameof(LaserWeaponConfig), menuName = "Configs/Weapons/" + nameof(LaserWeaponConfig))]
    public class LaserWeaponConfig : SpecificWeaponConfig
    {
        [field: SerializeField] public ProjectileConfig LaserProjectile { get; private set; }
        [field: SerializeField, Min(0.1f)] public float Long_Dlina { get; private set; } = 1f;
        [field: SerializeField, Min(0.1f)] public float MaxActiveTime_M { get; private set; } = 1f;
        [field: SerializeField, Min(0.1f)] public float ActiveTime_K { get; private set; } = 1f;
        [field: SerializeField, Range(0f, 1f)] public float Koefficient_X { get; private set; } = 1f;
    }
}
