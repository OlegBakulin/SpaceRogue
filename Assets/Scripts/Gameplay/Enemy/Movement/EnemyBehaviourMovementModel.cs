using Gameplay.Player;
using System.Threading;
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
<<<<<<< Updated upstream
=======
            _playerTransform = playerView.gameObject.transform;
            _enemyTransform = _view.gameObject.transform;
            EntryPoint.SubscribeToUpdate(RotateTowardsPlayer);
>>>>>>> Stashed changes
        }

        public void MoveForward()
        {
<<<<<<< Updated upstream
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
=======
            _movementModel.Accelerate(true);
>>>>>>> Stashed changes
        }

        public void MoveBackward()
        {
<<<<<<< Updated upstream
            if (_movementModel.CurrentSpeed > 0)
            {
                StopMoving();
                return;
            }
            
=======
>>>>>>> Stashed changes
            _movementModel.Accelerate(false);
        }
        
        public void RotateTowardsPlayer()
        {
<<<<<<< Updated upstream
            Vector3 currentDirection = _view.transform.position - Vector3.up;
            Vector3 direction = _view.transform.position - _player.transform.position;
            Debug.Log(currentDirection);
            Debug.Log(direction);
=======
            direction = _enemyTransform.position - _playerTransform.position;
            float hipotenyzeEnemyPlayer = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
            float angelEnemyPlayerGlobalX = Mathf.Asin(direction.y / hipotenyzeEnemyPlayer);
            float q = Vector3.Angle(_view.transform.position, _player.transform.position);
            float a = Vector3.AngleBetween(_view.transform.position, _player.transform.position);
            float s = Vector3.SignedAngle(_view.transform.position, _player.transform.position, Vector3.up);
            //if (direction.z < Mathf.PI / 2) direction.z = -direction.z;
            //int negativePozitive = direction.z < 0 ? -1 : 1;
            //direction.z = _view.transform.rotation.z < direction.z * Mathf.PI / 2 + 10 & _view.transform.rotation.z > direction.z * Mathf.PI / 2 + -10 ? 0 : negativePozitive * (direction.z - 0.2f);
            if (RotationClamp(_view.transform.rotation.z, angelEnemyPlayerGlobalX))
            {
                StopTurning();
            }
            else
            {
                _movementModel.SetTurnMultiplier(angelEnemyPlayerGlobalX);
            }
                //    (Mathf.Clamp(_view.transform.rotation.z, direction.z - 0.2f, direction.z + 0.2f) == _view.transform.rotation.z 
                //||  Mathf.Clamp(-_view.transform.rotation.z, direction.z - 0.2f, direction.z + 0.2f) == -_view.transform.rotation.z) 

               // ? 0f : direction.z;
        }

        private bool RotationClamp(float rotateEnemy, float needRotateEnemy)
        {
            return ((Mathf.Clamp(_view.transform.rotation.z, needRotateEnemy - 0.2f, needRotateEnemy + 0.2f) == _view.transform.rotation.z)
                || Mathf.Clamp(_view.transform.rotation.z, -needRotateEnemy - 0.2f, -needRotateEnemy + 0.2f) == _view.transform.rotation.z);
>>>>>>> Stashed changes
        }

        public void RotateByRandomAngle()
        {
<<<<<<< Updated upstream
            //10-20 degree rotation
=======
            direction = new Vector3(((float)random.Next(-100, 100)), ((float)random.Next(-100, 100)), ((float)random.Next(-100, 100)));
        }

        public void MoveAlongPlayer()
        {
            direction = (_enemyTransform.position - _playerTransform.position).normalized;
            Vector3 _alongPlayer = 
                (Mathf.Sqrt((direction.x + _playerTransform.TransformDirection(10, 0, 0).x) * (direction.x + _playerTransform.TransformDirection(10, 0, 0).x) + (direction.y + _playerTransform.TransformDirection(10, 0, 0).y) * (direction.y + _playerTransform.TransformDirection(10, 0, 0).y)) 
                > (Mathf.Sqrt((direction.x + _playerTransform.TransformDirection(-10, 0, 0).x) * (direction.x + _playerTransform.TransformDirection(-10, 0, 0).x) + (direction.y + _playerTransform.TransformDirection(-10, 0, 0).y) * (direction.y + _playerTransform.TransformDirection(-10, 0, 0).y)))) 
                ? (_playerTransform.position + _playerTransform.TransformDirection(10, 0, 0)) 
                : (_playerTransform.position + _playerTransform.TransformDirection(-10, 0, 0));
            direction = (_enemyTransform.position - _alongPlayer).normalized;
            float hipotenyze = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
            direction.z = Mathf.Acos(direction.x / hipotenyze);
        }

        public void RotateEnemy()
        {
            _enemyTransform.rotation = Quaternion.RotateTowards(_enemyTransform.rotation, Quaternion.LookRotation(direction, Vector3.forward), _speedRotation);
            _enemyTransform.rotation = new Quaternion (0, 0, _enemyTransform.rotation.z, _enemyTransform.rotation.w);
>>>>>>> Stashed changes
        }

        public void StopMoving()
        {
<<<<<<< Updated upstream
            if (_movementModel.CurrentSpeed > _movementModel.StoppingSpeed)
=======
            if (_movementModel.CurrentSpeed > 0.2f) 
>>>>>>> Stashed changes
            {
                _movementModel.Accelerate(false);
                return;
            }
<<<<<<< Updated upstream
            
            if (_movementModel.CurrentSpeed < -_movementModel.StoppingSpeed)
            {
                _movementModel.Accelerate(true);
=======

            if (_movementModel.CurrentSpeed < 0.2f) 
            {
                _movementModel.Accelerate(false);
>>>>>>> Stashed changes
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