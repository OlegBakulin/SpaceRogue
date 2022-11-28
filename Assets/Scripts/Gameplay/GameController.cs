using Abstracts;
using Gameplay.Camera;
using Gameplay.Enemy;
using Gameplay.GameState;
using Gameplay.Player;
using Gameplay.Space;
using UI.Game;
using UnityEngine;

namespace Gameplay
{
    public class GameController : BaseController
    {
        private readonly CurrentState _currentState;
        private readonly PlayerController _playerController;
        private readonly SpaceController _spaceController;
        private readonly EnemyForcesController _enemyForcesController;
        private readonly CameraController _cameraController;
        private readonly GameUIController _gameUIController;

        public GameController(CurrentState currentState, Canvas mainUICanvas)
        {
            _currentState = currentState;
            _gameUIController = new GameUIController(mainUICanvas, ExitToMenu);
            AddController(_gameUIController);
            
            _playerController = new PlayerController();
            AddController(_playerController);
            _playerController.PlayerDestroyed += OnPlayerDestroyed;

            _cameraController = new CameraController(_playerController);
            AddController(_cameraController);

            _spaceController = new SpaceController();
            AddController(_spaceController);

            _enemyForcesController = new EnemyForcesController(_playerController);
            AddController(_enemyForcesController);
        }

        private void OnPlayerDestroyed()
        {
            _gameUIController.AddDestroyPlayerMessage();
        }

        public void ExitToMenu() 
        {
            _currentState.CurrentGameState.Value = GameState.GameState.Menu;
        }
    }
}