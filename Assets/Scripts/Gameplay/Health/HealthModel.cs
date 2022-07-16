using System;
using Scriptables.Health;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Health
{
    public class HealthModel
    {
        public event Action OnPlayerDestroyed = () => { };
        public event Action OnDamageTaken = () => { };
        
        public SubscribedProperty<float> CurrentHealth { get; }
        public SubscribedProperty<float> MaximumHealth { get; }
        public SubscribedProperty<float> CurrentShield { get; }
        public SubscribedProperty<float> MaximumShield { get; }

        private float _currentShieldCooldown;
        private readonly float _healthRegenAmount;
        private readonly float _shieldCooldown;

        private float RegenAmountPerDeltaTime => _healthRegenAmount * Time.deltaTime;

        public HealthModel(HealthConfig healthConfig, ShieldConfig shieldConfig)
        {
            MaximumHealth = new SubscribedProperty<float>(healthConfig.MaximumHealth);
            CurrentHealth = new SubscribedProperty<float>(healthConfig.StartingHealth);
            _healthRegenAmount = healthConfig.HealthRegen;

            MaximumShield = new SubscribedProperty<float>(shieldConfig.ShieldAmount);
            CurrentShield = new SubscribedProperty<float>(shieldConfig.ShieldAmount);
            _shieldCooldown = shieldConfig.Cooldown;
            _currentShieldCooldown = 0f;
        }

        internal void TakeDamage(float damageAmount)
        {
            OnDamageTaken();
            
            if (CurrentShield.Value > 0)
            {
                if (CurrentShield.Value < damageAmount)
                {
                    CurrentShield.Value = 0;
                    TakeHealthDamage(damageAmount - CurrentShield.Value);
                }
                else
                {
                    TakeShieldDamage(damageAmount);
                }

                return;
            }
            
            TakeHealthDamage(damageAmount);
            StartShieldCooldown();
        }

        internal void UpdateState()
        {
            if (CurrentHealth.Value < MaximumHealth.Value)
            {
                CurrentHealth.Value += RegenAmountPerDeltaTime;
            }

            if (_currentShieldCooldown > 0.0f)
            {
                CooldownShield();
            }

            if (_currentShieldCooldown == 0.0f && CurrentShield.Value < MaximumShield.Value)
            {
                RefreshShield();
            }
        }

        private void TakeShieldDamage(float damageAmount)
        {
            CurrentShield.Value -= damageAmount;
        }

        private void TakeHealthDamage(float damageAmount)
        {
            if (damageAmount >= CurrentHealth.Value)
            {
                CurrentHealth.Value = 0.0f;
                OnPlayerDestroyed();
                return;
            }

            CurrentHealth.Value -= damageAmount;
        }

        private void StartShieldCooldown()
        {
            _currentShieldCooldown = _shieldCooldown;
        }

        private void CooldownShield()
        {
            _currentShieldCooldown -= Time.deltaTime;
        }

        private void RefreshShield()
        {
            CurrentShield.Value = MaximumShield.Value;
        }
    }
}