using System;
using Gameplay.Health;
using UnityEngine;

namespace Gameplay.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : MonoBehaviour, IDamageableView
    {
        public event Action<float> DamageTaken = (float _) => { };
    }
}