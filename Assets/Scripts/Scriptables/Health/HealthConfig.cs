using UnityEngine;

namespace Scriptables.Health
{
    [CreateAssetMenu(fileName = nameof(HealthConfig), menuName = "Configs/Health/" + nameof(HealthConfig))]
    public class HealthConfig : ScriptableObject
    {
        [field: SerializeField, Min(1f)] public float MaximumHealth { get; private set; } = 1.0f;
        [field: SerializeField, Min(1f)] public float StartingHealth { get; private set; } = 1.0f;
        [field: SerializeField, Min(0f)] public float HealthRegen { get; private set; } = 0f;
    }
}