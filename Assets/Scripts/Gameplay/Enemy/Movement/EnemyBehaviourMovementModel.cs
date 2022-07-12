using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Enemy.Movement
{
    public class EnemyBehaviourMovementModel
    {
        private readonly EnemyView _view;
        private readonly EnemyMovementModel _movementModel;
        private readonly PlayerView _player;
        
        public EnemyBehaviourMovementModel(EnemyMovementModel movementModel, EnemyView view, PlayerView playerView)
        {
            _movementModel = movementModel;
            _view = view;
            _player = playerView;
        }
        
        //TODO implement behaviour in all bottom methods

        public void MoveForward()
        {
        }

        public void MoveBackward()
        {
            
        }

        public void RotateTowardsPlayer()
        {
            Vector3 direction = _view.transform.position - _player.transform.position;
        }

        public void RotateByRandomAngle()
        {
            //10-20 degree rotation
        }

        public void StopMoving()
        {
            
        }

        public void StopTurning()
        {
            
        }
    }
}