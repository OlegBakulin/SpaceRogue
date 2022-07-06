using Abstracts;
using Gameplay.Enemy;
using Gameplay.Camera;
using Gameplay.GameState;
using Gameplay.Player;
using Gameplay.Space;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private readonly CurrentState _currentState;
        private readonly PlayerController _playerController;
        private readonly SpaceController _spaceController;
        private readonly EnemyForcesController _enemyForcesController;
        private readonly CameraController _cameraController;
        private readonly CanvasController _canvasController;

        public GameController(CurrentState currentState)
        {
            _currentState = currentState;
            
            _playerController = new PlayerController();
            AddController(_playerController);

            _cameraController = new CameraController(_playerController.View);
            AddController(_cameraController);

            _spaceController = new SpaceController();
            AddController(_spaceController);

            _enemyForcesController = new EnemyForcesController(_playerController.View);
            AddController(_enemyForcesController);
        }
    }
}