using Abstracts;
using System;
using UI.Abstracts;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ResourceManagement;

namespace UI.Game
{
    public class GameUIController : BaseController
    {
        public static PlayerStatusBarView PlayerStatusBarView { get; private set; }
        public static PlayerSpeedometerView PlayerSpeedometerView { get; private set; }
        public static DestroyPlayerView DestroyPlayerViewComponent { get; private set; }

        private readonly Canvas _mainCanvas;

        private Canvas _playerStatusBarCanvas;
        private Canvas _playerSpeedometerCanvas;
        private Canvas _playerDestroyPlayerCanvas;

        private PlayerStatusBarView _playerStatusBarView;
        private PlayerSpeedometerView _playerSpeedometerView;
        private DestroyPlayerView _playerDestroyPlayerView;

        private readonly ResourcePath _playerStatusBarCanvasPath = new("Prefabs/Canvas/Game/StatusBarCanvas");
        private readonly ResourcePath _playerSpeedometerCanvasPath = new("Prefabs/Canvas/Game/SpeedometerCanvas");
        private readonly ResourcePath _playerDestroyPlayerCanvasPath = new("Prefabs/Canvas/Game/DestroyPlayerCanvas");

        public GameUIController(Canvas mainCanvas)
        {
            _mainCanvas = mainCanvas;
            
            AddPlayerStatusBar();
            AddPlayerSpeedometer();
            AddDestroyPlayerMessage();
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

        protected override void OnDispose()
        {
        }
            
        public void AddDestroyPlayerMessage()
        {
            _playerDestroyPlayerCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_playerDestroyPlayerCanvasPath, _mainCanvas.transform);
            _playerDestroyPlayerView = _playerDestroyPlayerCanvas.GetComponent<DestroyPlayerView>();
            DestroyPlayerViewComponent = _playerDestroyPlayerView;
            DestroyPlayerViewComponent.gameObject.GetComponentInChildren<Button>().onClick.AddListener(QuitGame);
            ActivatorButtonDestroyPlayer(false);
            AddGameObject(_playerDestroyPlayerCanvas.gameObject);

        }

        public static void ActivatorButtonDestroyPlayer (Boolean activeButtonTrueOrFalse)
        {
            DestroyPlayerViewComponent.gameObject.SetActive(activeButtonTrueOrFalse);
        }

        private void QuitGame() => Application.Quit();
    }
}