using Abstracts;
using Gameplay.Player;
using UnityEngine;
using Utilities.Extensions;

namespace Gameplay.Camera
{
    public class CameraController : BaseController
    {
        private readonly CameraView _cameraView;
        private readonly PlayerController _playerController;
        private const int CameraZAxisOffset = -10;
        private Vector3 playerPosition;
        private Transform _cameraPosition;
        private Transform _playerPosition;

        public CameraController(PlayerController playerController)
        {
            _cameraView = UnityEngine.Camera.main!.GetComponent<CameraView>();
            _playerController = playerController;
            _cameraPosition = _cameraView.gameObject.transform;
            _playerPosition = _playerController.View.gameObject.transform;
            EntryPoint.SubscribeToUpdate(FollowPlayer);
            _playerController.PlayerDestroyed += OnPlayerDestroyed;
        }
        
        public void OnPlayerDestroyed()
        {
            EntryPoint.UnsubscribeFromUpdate(FollowPlayer);
        }

        private void FollowPlayer()
        {
            playerPosition = _playerPosition.position;
            _cameraPosition.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + CameraZAxisOffset);
        }
    }
}