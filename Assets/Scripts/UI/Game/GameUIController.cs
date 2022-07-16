using Abstracts;
using UnityEngine;
using Utilities.ResourceManagement;

namespace UI.Game
{
    public class GameUIController : BaseController
    {
        public static PlayerStatusBarView PlayerStatusBarView { get; private set; }
        public static PlayerSpeedometerView PlayerSpeedometerView { get; private set; }

        private readonly Canvas _mainCanvas;
        private Canvas _playerStatusBarCanvas;
        private Canvas _playerSpeedometerCanvas;
        private PlayerStatusBarView _playerStatusBarView;
        private PlayerSpeedometerView _playerSpeedometerView;

        private readonly ResourcePath _playerStatusBarCanvasPath = new("Prefabs/Canvas/Game/StatusBarCanvas");
        private readonly ResourcePath _playerSpeedometerCanvasPath = new("Prefabs/Canvas/Game/SpeedometerCanvas");

        public GameUIController(Canvas mainCanvas)
        {
            _mainCanvas = mainCanvas;
            
            AddPlayerStatusBar();
            AddPlayerSpeedometer();
        }

        private void AddPlayerStatusBar()
        {
            _playerStatusBarCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_playerStatusBarCanvasPath, _mainCanvas.transform);
            _playerStatusBarView = _playerStatusBarCanvas.GetComponent<PlayerStatusBarView>();
            PlayerStatusBarView = _playerStatusBarView;
            AddGameObject(_playerStatusBarCanvas.gameObject);
        }

        private void AddPlayerSpeedometer()
        {
            _playerSpeedometerCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_playerSpeedometerCanvasPath, _mainCanvas.transform);
            _playerSpeedometerView = _playerSpeedometerCanvas.GetComponent<PlayerSpeedometerView>();
            PlayerSpeedometerView = _playerSpeedometerView;
            AddGameObject(_playerSpeedometerCanvas.gameObject);
        }
    }
}