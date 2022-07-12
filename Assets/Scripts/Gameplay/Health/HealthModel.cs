using Scriptables;
using Scriptables.Health;
using Scriptables.Modules;

namespace Gameplay.Health
{
    public class HealthModel
    {
        public float CurrentHealth => _currentHealth;
        public float MaximumHealth => _maximumHealth;
        public float CurrentShield => _currentShield;
        public float MaximumShield => _maximumShield;
        
        private readonly float _currentHealth;
        private readonly float _healthRegenAmount;
        private readonly float _maximumHealth;
        
        private readonly float _currentShield;
        private readonly float _maximumShield;
        private readonly float _shieldCooldown;
        private readonly float _currentShieldCooldown;

        public HealthModel(HealthConfig healthConfig, ShieldConfig shieldConfig)
        {
            _maximumHealth = healthConfig.MaximumHealth;
            _currentHealth = healthConfig.StartingHealth;
            _healthRegenAmount = healthConfig.HealthRegen;

            _maximumShield = shieldConfig.ShieldAmount;
            _currentShield = shieldConfig.ShieldAmount;
            _shieldCooldown = shieldConfig.Cooldown;
            _currentShieldCooldown = 0f;
        }
    }
}