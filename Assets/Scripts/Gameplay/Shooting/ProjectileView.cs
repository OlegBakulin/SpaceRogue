using System;
using Gameplay.Damage;
using UnityEngine;

namespace Gameplay.Shooting
{
    public class ProjectileView : MonoBehaviour, IDamagingView
    {
        public event Action CollisionEnter = () => { };
        public DamageModel DamageModel { get; private set; }

        public void Init(DamageModel damageModel)
        {
            DamageModel = damageModel;
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            CollisionEnter();
        }
    }
}