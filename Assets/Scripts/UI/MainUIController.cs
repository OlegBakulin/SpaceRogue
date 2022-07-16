using Abstracts;
using UnityEngine;
using Utilities.ResourceManagement;

namespace UI
{
    public class MainUIController : BaseController
    {
        public Canvas MainCanvas { get; }
        
        private readonly ResourcePath _uiCameraPath = new("Prefabs/Canvas/UICamera");
        private readonly ResourcePath _mainCanvasPath = new("Prefabs/Canvas/MainCanvas");

        public MainUIController(Transform uiPosition)
        {
            var uiCamera = ResourceLoader.LoadPrefabAsChild<Camera>(_uiCameraPath, uiPosition);
            MainCanvas = ResourceLoader.LoadPrefabAsChild<Canvas>(_mainCanvasPath, uiPosition);
            MainCanvas.worldCamera = uiCamera;
            
            AddGameObject(uiCamera.gameObject);
            AddGameObject(MainCanvas.gameObject);
        }
    }
}