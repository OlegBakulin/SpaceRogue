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
        System.Random random = new System.Random();
        float gradys;


        public EnemyBehaviourMovementModel(EnemyMovementModel movementModel, EnemyView view, PlayerView playerView)
        {
            _movementModel = movementModel;
            _speedRotation = _movementModel.CurrentTurnRate;
            _view = view;
            _player = playerView;
            _playerTransform = playerView.gameObject.transform;
            _enemyTransform = _view.gameObject.transform;
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
            Vector3 direction = (_enemyTransform.position - _playerTransform.position).normalized;
            float hipotenyze = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
            direction.z = Mathf.Acos(direction.x / hipotenyze);
            RotateEnemy(direction);
        }

        public void RotateByRandomAngle()
        {
            Vector3 direction = new Vector3(((float)random.Next(-100, 100)), ((float)random.Next(-100, 100)), ((float)random.Next(-100, 100)));
            RotateEnemy(direction);
        }

        public void RotateEnemy(Vector3 direction)
        {
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