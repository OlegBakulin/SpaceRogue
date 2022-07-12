using Gameplay.Shooting;
using UnityEngine;

namespace Scriptables.Modules
{
    [CreateAssetMenu(fileName = nameof(TurretModuleConfig), menuName = "Configs/Modules/" + nameof(TurretModuleConfig))]
    public class TurretModuleConfig : BaseModuleConfig
    {
        [field: SerializeField, Min(0.1f)] public float Cooldown { get; private set; }
        [field: SerializeField] public ProjectileConfig ProjectileConfig { get; private set; }
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        
    }
}