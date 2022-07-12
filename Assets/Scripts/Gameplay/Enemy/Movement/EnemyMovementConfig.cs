using System;
using UnityEngine;

namespace Gameplay.Enemy.Movement
{
    [Serializable]
    public class EnemyMovementConfig
    {
        [Header("Speed")] 
        [Min(0.1f)]
        [SerializeField] public float maximumSpeed = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float maximumBackwardSpeed = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float accelerationTime = 0.1f;

        [Header("Turn speed")] 
        [Min(0.1f)]
        [SerializeField] public float startingTurnSpeed = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float maximumTurnSpeed = 0.1f;
        [Min(0.1f)]
        [SerializeField] public float turnAccelerationTime = 0.1f;
    }
}