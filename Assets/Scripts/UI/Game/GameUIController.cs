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
        private readonly Canvas _playerStatusBarCanvas;
        private readonly Canvas _playerSpeedometerCanvas;
        private readonly PlayerStatusBarView _playerStatusBarView;
        private readonly PlayerSpeedometerView _playerSpeedometerView;

        private readonly ResourcePath _playerStatusBarCanvasPath = new("Prefabs/Canvas/Game/StatusBarCanvas");
        private readonly ResourcePath _playerSpeedometerCanvasPath = new("Prefabs/Canvas/Game/SpeedometerCanvas");

        public GameUIController(Canvas mainCanvas)
        {
            _mainCanvas = mainCanvas;
            
            _playerStatusBarCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_playerStatusBarCanvasPath, _mainCanvas.transform);
            _playerStatusBarView = _playerStatusBarCanvas.GetComponent<PlayerStatusBarView>();
            PlayerStatusBarView = _playerStatusBarView;

            _playerSpeedometerCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_playerSpeedometerCanvasPath, _mainCanvas.transform);
            _playerSpeedometerView = _playerSpeedometerCanvas.GetComponent<PlayerSpeedometerView>();
            PlayerSpeedometerView = _playerSpeedometerView;
            
            AddGameObject(_playerStatusBarCanvas.gameObject);
            AddGameObject(_playerSpeedometerCanvas.gameObject);
        }
    }
}