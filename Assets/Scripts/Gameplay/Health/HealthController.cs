using Abstracts;
using Scriptables;
using Scriptables.Modules;

namespace Gameplay.Health
{
    public class HealthController : BaseController
    {
        private readonly HealthModel _healthModel;
        private readonly PlayerStatusBarController playerStatusBarController;


        public HealthController(HealthConfig healthConfig, ShieldModuleConfig shieldConfig)
        {
            _healthModel = new HealthModel(healthConfig, shieldConfig);

            playerStatusBarController = new PlayerStatusBarController(_healthModel);
            AddController(playerStatusBarController);

            EntryPoint.SubscribeToLateUpdate(playerStatusBarController.UpdateHealtShieldToolBar);
        }

        protected override void OnDispose()
        {
            EntryPoint.SubscribeToLateUpdate(playerStatusBarController.UpdateHealtShieldToolBar);
        }
    }
}