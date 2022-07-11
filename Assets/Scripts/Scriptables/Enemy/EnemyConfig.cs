using System;
using Abstracts;
using Gameplay.Enemy;
using Gameplay.Enemy.Behaviour;
using Gameplay.Enemy.Movement;
using Scriptables.Modules;
using UnityEngine;

namespace Scriptables.Enemy
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = "Configs/Enemy/" + nameof(EnemyConfig))]
    public class EnemyConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public EnemyView Prefab { get; private set; }
        [field: SerializeField] public TurretModuleConfig Weapon { get; private set; }
        [field: SerializeField] public EnemyMovementConfig Movement { get; private set; }
        [field: SerializeField] public EnemyBehaviourConfig Behaviour { get; private set; }
    }
}