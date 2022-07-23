using Abstracts;
using Scriptables.Health;
using UI.Game;

namespace Gameplay.Health
{
    public class HealthController : BaseController
    {
        private readonly PlayerStatusBarView _statusBarView;
        private readonly HealthWithShieldModel _healthModel;

        public HealthController(HealthConfig healthConfig, ShieldConfig shieldConfig)
        {
            _healthModel = new HealthWithShieldModel(healthConfig, shieldConfig);
            _statusBarView = GameUIController.PlayerStatusBarView;
            
            _statusBarView.HealthBar.Init(0.0f, _healthModel.MaximumHealth.Value, _healthModel.CurrentHealth.Value);
            _statusBarView.ShieldBar.Init(0.0f, _healthModel.MaximumShield.Value, _healthModel.CurrentShield.Value);
            _healthModel.CurrentHealth.Subscribe(_statusBarView.HealthBar.UpdateValue);
            _healthModel.CurrentShield.Subscribe(_statusBarView.ShieldBar.UpdateValue);
            EntryPoint.SubscribeToUpdate(_healthModel.UpdateState);
        }

        public HealthController(HealthConfig healthConfig)
        {
        }

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(_healthModel.UpdateState);
            _healthModel.CurrentHealth.Unsubscribe(_statusBarView.HealthBar.UpdateValue);
            _healthModel.CurrentShield.Unsubscribe(_statusBarView.ShieldBar.UpdateValue);
        }
    }
}