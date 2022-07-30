using System;
using Gameplay.Damage;
using Gameplay.Health;
using UnityEngine;

namespace Abstracts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class UnitView : MonoBehaviour, IDamageableView
    {
        public event Action<DamageModel> DamageTaken = (DamageModel _) => { };

        public void OnTriggerEnter2D(Collider2D other)
        {
            var damageComponent = other.gameObject.GetComponent<IDamagingView>();
            if (damageComponent is not null)
            {
                TakeDamage(damageComponent);
            }
        }

        public void TakeDamage(IDamagingView damageComponent)
        {
            DamageTaken(damageComponent.DamageModel);
        }
    }
}