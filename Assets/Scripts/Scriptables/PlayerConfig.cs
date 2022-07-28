using Gameplay.Health;
using Gameplay.Player.Inventory;
using Scriptables.Modules;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/Player/" + nameof(PlayerConfig))]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerInventoryConfig Inventory { get; private set; }
        [field: SerializeField] public HealthConfig HealthConfig { get; private set; }
    }
}