using Abstracts;
using Gameplay.Health;
using UI.Game;
using UnityEngine;
using Utilities.ResourceManagement;

namespace UI
{
    public class CanvasController : BaseController
    {
        private readonly ResourcePath canvasPath = new("Prefabs/Canvas/Canvas");

        private readonly float _currentSpeed;
        private readonly float _maximumSpeed;
        private readonly HealthController _healthController;
        private readonly PlayerStatusBarController playerStatusBarController;
        private readonly SpeedometerController speedometerController;
        
        public CanvasController(HealthController healthController, float currentSpeed, float maximumSpeed)
        {
            _currentSpeed = currentSpeed;
            _maximumSpeed = maximumSpeed;
            _healthController = healthController;

            Canvas canvas = ResourceLoader.LoadPrefab<Canvas>(canvasPath);

            playerStatusBarController = new PlayerStatusBarController(_healthController.HealthModel, canvas);
            AddController(playerStatusBarController);

            speedometerController = new SpeedometerController(canvas, _currentSpeed, _maximumSpeed);
            AddController(speedometerController);
        }
    }
}
