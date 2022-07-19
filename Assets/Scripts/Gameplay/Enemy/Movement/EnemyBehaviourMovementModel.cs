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


        public EnemyBehaviourMovementModel(EnemyMovementModel movementModel, EnemyView view, PlayerView playerView)
        {
            _movementModel = movementModel;
            _speedRotation = _movementModel.CurrentTurnRate;
            _view = view;
            _player = playerView;
            _playerTransform = playerView.gameObject.transform;
            _enemyTransform = _view.gameObject.transform;
            EntryPoint.SubscribeToUpdate(RotateTowardsPlayer);
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
            Vector3 direction = (_view.transform.position - _player.gameObject.transform.position).normalized;
            float hipotenyze = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
            direction.z = Mathf.Acos(direction.x / hipotenyze);
            _enemyTransform.rotation = Quaternion.RotateTowards(_enemyTransform.rotation, Quaternion.LookRotation(direction, Vector3.forward), _speedRotation);
            _enemyTransform.rotation = new Quaternion (0, 0, _enemyTransform.rotation.z, _enemyTransform.rotation.w);
        }

        public void RotateByRandomAngle()
        {
            //10-20 degree rotation
            System.Random random = new System.Random();
            int gradys = random.Next(-10, 10);
            gradys = gradys < 0 ? gradys - 10 : gradys + 10;
            Vector3 direction = _view.transform.position - _player.transform.position - new Vector3(0, 0, gradys);

            _enemyTransform.rotation = Quaternion.RotateTowards(_enemyTransform.rotation, Quaternion.LookRotation(direction), _movementModel.CurrentTurnRate);
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