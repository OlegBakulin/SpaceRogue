using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Enemy.Movement
{
    public class EnemyBehaviourMovementModel
    {
        private readonly EnemyView _view;
        private readonly EnemyMovementModel _movementModel;
        private readonly PlayerView _player;
        private Transform _playerTransform;
        private Transform _enemyTransform;
        private float _speedRotation;
        private System.Random random = new System.Random();
        private Vector3 direction;


        public EnemyBehaviourMovementModel(EnemyMovementModel movementModel, EnemyView view, PlayerView playerView)
        {
            _movementModel = movementModel;
            _speedRotation = _movementModel.CurrentTurnRate;
            _view = view;
            _player = playerView;
            _playerTransform = playerView.gameObject.transform;
            _enemyTransform = _view.gameObject.transform;
            // Вставить вызов функции для нахождения объекта для напрвления поворота
            EntryPoint.SubscribeToUpdate(RotateEnemy);
        }
        
        //TODO implement behaviour in all bottom methods

        public void MoveForward()
        {
            float currentSpeed = _movementModel.CurrentSpeed;
            if (currentSpeed != 0)
            {
                var forwardDirection = _enemyTransform.TransformDirection(Vector3.up);
                _enemyTransform.position += forwardDirection * currentSpeed * Time.deltaTime;
            }
        }

        public void MoveBackward()
        {
            
        }

        public void RotateTowardsPlayer()
        {
            direction = (_enemyTransform.position - _playerTransform.position).normalized;
            float hipotenyze = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
            direction.z = Mathf.Acos(direction.x / hipotenyze);
        }

        public void RotateByRandomAngle()
        {
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
            RotateTowardsPlayer();
            _enemyTransform.rotation = Quaternion.RotateTowards(_enemyTransform.rotation, Quaternion.LookRotation(direction, Vector3.forward), _speedRotation);
            _enemyTransform.rotation = new Quaternion (0, 0, _enemyTransform.rotation.z, _enemyTransform.rotation.w);
        }

            public void StopMoving()
        {
            _movementModel.StopMoving();
        }

        public void StopTurning()
        {
            _movementModel.StopTurning();
        }
    }
}