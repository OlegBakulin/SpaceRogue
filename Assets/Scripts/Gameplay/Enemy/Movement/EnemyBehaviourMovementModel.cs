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

        public void MoveForward()
        {
            if (_movementModel.CurrentSpeed < 0)
            {
                StopMoving();
                return;
            }
            
            _movementModel.Accelerate(true);
        }
        
        public void MoveForwardAtLowSpeed()
        {
            if (_movementModel.CurrentSpeed < 0)
            {
                StopMoving();
                return;
            }
            
            if (_movementModel.CurrentSpeed <= _movementModel.MaximumSpeed / 2) 
                _movementModel.Accelerate(true);
        }

        public void MoveBackward()
        {
            if (_movementModel.CurrentSpeed > 0)
            {
                StopMoving();
                return;
            }
            
            _movementModel.Accelerate(false);
        }
        
        public void RotateTowardsPlayer()
        {
            Vector3 currentDirection = _view.transform.position - Vector3.up;
            Vector3 direction = _view.transform.position - _player.transform.position;
            Debug.Log(currentDirection);
            Debug.Log(direction);
        }

        public void RotateByRandomAngle()
        {
            //10-20 degree rotation
        }

        public void StopMoving()
        {
            if (_movementModel.CurrentSpeed > _movementModel.StoppingSpeed)
            {
                _movementModel.Accelerate(false);
                return;
            }
            
            if (_movementModel.CurrentSpeed < -_movementModel.StoppingSpeed)
            {
                _movementModel.Accelerate(true);
                return;
            }
            
            _movementModel.StopMoving();
        }

        public void StopTurning()
        {
            _movementModel.StopTurning();
        }
    }
}