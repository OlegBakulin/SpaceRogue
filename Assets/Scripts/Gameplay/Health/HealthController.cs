using Abstracts;
using Scriptables.Health;
using UI.Game;

namespace Gameplay.Health
{
    public class HealthController : BaseController
    {
        private readonly PlayerStatusBarView _statusBarView;
        private readonly HealthModel _healthModel;

        public HealthController(HealthConfig healthConfig, ShieldConfig shieldConfig)
        {
            _healthModel = new HealthModel(healthConfig, shieldConfig);
            _statusBarView = GameUIController.PlayerStatusBarView;
            
            _statusBarView.HealthSlider.Init(0.0f, _healthModel.MaximumHealth.Value, _healthModel.CurrentHealth.Value);
            _statusBarView.ShieldSlider.Init(0.0f, _healthModel.MaximumShield.Value, _healthModel.CurrentShield.Value);
            _healthModel.CurrentHealth.Subscribe(_statusBarView.HealthSlider.UpdateValue);
            _healthModel.CurrentShield.Subscribe(_statusBarView.ShieldSlider.UpdateValue);
            EntryPoint.SubscribeToUpdate(_healthModel.UpdateState);
        }

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(_healthModel.UpdateState);
            _healthModel.CurrentHealth.Unsubscribe(_statusBarView.HealthSlider.UpdateValue);
            _healthModel.CurrentShield.Unsubscribe(_statusBarView.ShieldSlider.UpdateValue);
        }
    }
}