using Abstracts;
using Gameplay.Enemy;
using Gameplay.Camera;
using Gameplay.GameState;
using Gameplay.Player;
using Gameplay.Space;
using UI.Game;
using UnityEngine;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private static CurrentState _currentState;
        private readonly PlayerController _playerController;
        private readonly SpaceController _spaceController;
        private readonly EnemyForcesController _enemyForcesController;
        private readonly CameraController _cameraController;
        private GameUIController _gameUIController;
        private readonly Canvas _mainUICanvas;

        public GameController(CurrentState currentState, Canvas mainUICanvas)
        {
            _currentState = currentState;
            _mainUICanvas = mainUICanvas;
            _gameUIController = new GameUIController(_mainUICanvas);
            AddController(_gameUIController);
            
            _spaceController = new SpaceController();
            AddController(_spaceController);

            _playerController = new PlayerController();
            AddController(_playerController);
            _playerController.PlayerDestroyed += OnPlayerDestroyed;

            _cameraController = new CameraController(_playerController.View);
            AddController(_cameraController);

            _enemyForcesController = new EnemyForcesController(_playerController.View);
            AddController(_enemyForcesController);
        }

        public void OnPlayerDestroyed()
        {
            _gameUIController.AddDestroyPlayerMessage();
        }

        public static void EditorStatusGameOnMenu() 
        {
            _currentState.CurrentGameState.Value = GameState.GameState.Menu;
        }
    }
}