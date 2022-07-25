using Abstracts;
using Scriptables.Health;
using UI.Game;

namespace Gameplay.Health
{
    public class HealthController : BaseController
    {
        private readonly HealthStatusBarView _statusBarView;
        private readonly BaseHealthModel _healthModel;

        public HealthController(HealthConfig healthConfig, ShieldConfig shieldConfig, HealthShieldStatusBarView statusBarView)
        {
            var healthModel = new HealthWithShieldModel(healthConfig, shieldConfig);
            
            statusBarView.HealthBar.Init(0.0f, healthModel.MaximumHealth.Value, healthModel.CurrentHealth.Value);
            statusBarView.ShieldBar.Init(0.0f, healthModel.MaximumShield.Value, healthModel.CurrentShield.Value);
            
            healthModel.CurrentHealth.Subscribe(statusBarView.HealthBar.UpdateValue);
            healthModel.CurrentShield.Subscribe(statusBarView.ShieldBar.UpdateValue);
            EntryPoint.SubscribeToUpdate(healthModel.UpdateState);

            _statusBarView = statusBarView;
            _healthModel = healthModel;
        }

        public HealthController(HealthConfig healthConfig, HealthStatusBarView statusBarView)
        {
            var healthModel = new HealthOnlyModel(healthConfig);
            statusBarView.HealthBar.Init(0.0f, healthModel.MaximumHealth.Value, healthModel.CurrentHealth.Value);
            healthModel.CurrentHealth.Subscribe(statusBarView.HealthBar.UpdateValue);
        }

        protected override void OnDispose()
        {
            if (_healthModel is HealthWithShieldModel healthShieldModel && _statusBarView is HealthShieldStatusBarView statusShieldBar) 
                healthShieldModel.CurrentShield.Unsubscribe(statusShieldBar.ShieldBar.UpdateValue);
            
            _healthModel.CurrentHealth.Unsubscribe(_statusBarView.HealthBar.UpdateValue);
            EntryPoint.UnsubscribeFromUpdate(_healthModel.UpdateState);
        }
    }
}