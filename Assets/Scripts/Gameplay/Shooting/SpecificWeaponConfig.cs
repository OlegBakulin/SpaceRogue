using System;
using Abstracts;
using UnityEngine;

namespace Gameplay.Shooting
{
    public abstract class SpecificWeaponConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField, Min(0.1f)] public float Cooldown { get; private set; }
    }
}