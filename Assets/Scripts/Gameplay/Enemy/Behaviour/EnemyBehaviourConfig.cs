using System;
using UnityEngine;

namespace Gameplay.Enemy.Behaviour
{
    [Serializable]
    public class EnemyBehaviourConfig
    {
        [field: SerializeField] public float PlayerDetectionRadius { get; private set; }
        [field: SerializeField] public float CallToArmsRadius { get; private set; }
        [field: SerializeField] public float ShootingDistance { get; private set; }
    }
}