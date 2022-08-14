using Abstracts;
using Gameplay.Player;
using UnityEngine;
using Utilities.Extensions;

namespace Gameplay.Camera
{
    public class CameraController : BaseController
    {
        private readonly CameraView _cameraView;
        private readonly PlayerView _playerView;
        private const int CameraZAxisOffset = -10;
        private Vector3 playerPosition;

        public CameraController(PlayerView playerView)
        {
            _cameraView = UnityEngine.Camera.main!.GetComponent<CameraView>();
            _playerView = playerView;
            EntryPoint.SubscribeToUpdate(FollowPlayer);
        }
        
        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(FollowPlayer);
        }

        private void FollowPlayer()
        {
            playerPosition = _playerView ? _playerView.gameObject.transform.position : playerPosition;
            _cameraView.gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + CameraZAxisOffset);
        }
    }
}