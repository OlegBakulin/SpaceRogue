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
        private float _currentDamageImmunityTime;
        private readonly float _healthRegenAmount;
        private readonly float _shieldCooldown;
        private const float DamageImmunityFrameLength = 0.5f;
        

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
            if (_currentDamageImmunityTime > 0.0f) return;

            StartDamageImmunityWindow();
            StartShieldCooldown();
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
        }

        internal void UpdateState()
        {
            if (_currentDamageImmunityTime >= 0.0f) CooldownDamageImmunityFrame();
            
            if (_currentShieldCooldown <= 0.0f && CurrentShield.Value < MaximumShield.Value)
            {
                RefreshShield();
            }
            
            if (CurrentHealth.Value < MaximumHealth.Value)
            {
                CurrentHealth.Value += RegenAmountPerDeltaTime;
            }

            if (_currentShieldCooldown > 0.0f)
            {
                CooldownShield();
            }
        }

        private void CooldownDamageImmunityFrame()
        {
            _currentDamageImmunityTime -= Time.deltaTime;
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

        private void StartDamageImmunityWindow()
        {
            _currentDamageImmunityTime = DamageImmunityFrameLength;
        }
    }
}