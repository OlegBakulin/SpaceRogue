using Abstracts;
using Scriptables.Health;

namespace Gameplay.Health
{
    public class HealthController : BaseController
    {
        public HealthModel HealthModel => _healthModel;
        private readonly HealthModel _healthModel;

        public HealthController(HealthConfig healthConfig, ShieldConfig shieldConfig)
        {
            _healthModel = new HealthModel(healthConfig, shieldConfig);
        }
    }
}