using System;
using Gameplay.Enemy.Movement;
using Gameplay.Movement;
using Gameplay.Player;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.Reactive.SubscriptionProperty;
using Utilities.Unity;
using Random = System.Random;

namespace Gameplay.Enemy.Behaviour
{
    public class EnemyRoamingBehaviour : EnemyBehaviour
    {
        private readonly MovementModel _movementModel;
        private readonly EnemyInputController _inputController;

        private readonly Random _random;
        private Vector3 _targetDirection;

        private float _rotationTimer;

        private const float RotationDelay = 2.0f;
        
        public EnemyRoamingBehaviour(
            SubscribedProperty<EnemyState> enemyState,
            EnemyView view,
            PlayerView playerView, 
            MovementModel movementModel, 
            EnemyInputController inputController) : base(enemyState, view, playerView)
        {
            _movementModel = movementModel;
            _inputController = inputController;
            _random = new Random();
            _targetDirection = RandomPicker.PickRandomAngle(180, new Random());
            _rotationTimer = RotationDelay;
        }
        
        protected override void OnUpdate()
        {
            MoveAtLowSpeed();
            TurnToRandomDirection();
        }

        private void MoveAtLowSpeed()
        {
            var quarterMaxSpeed = _movementModel.MaxSpeed / 4;
            switch (CompareSpeeds(_movementModel.CurrentSpeed, quarterMaxSpeed))
            {
                case -1: { _inputController.Accelerate(); return; }
                case 0: { _inputController.HoldSpeed(); return; }
                case 1: { _inputController.Decelerate(); return; }
            }
        }

        private int CompareSpeeds(float currentSpeed, float targetSpeed)
        {
            if (UnityHelper.Approximately(currentSpeed, targetSpeed, 0.1f)) return 0;
            if (currentSpeed < targetSpeed) return -1;
            return 1;
        }

        private void TurnToRandomDirection()
        {
            var currentDirection = View.transform.TransformDirection(Vector3.up);

            if (UnityHelper.Approximately(_targetDirection, currentDirection, 0.1f))
            {
                _inputController.StopTurning();
            }
            else
            {
                HandleTurn(currentDirection);
            }
            
            TickDownTimer();
        }

        private void HandleTurn(Vector3 currentDirection)
        {
            if ((currentDirection - _targetDirection).x < 0)
            {
                _inputController.TurnLeft();
            }
            else
            {
                _inputController.TurnRight();
            }
        }

        private void TickDownTimer()
        {
            if (_rotationTimer <= 0.0f)
            {
                _rotationTimer = RotationDelay;
                _targetDirection = RandomPicker.PickRandomAngle(360, new Random());
            }
            else
            {
                _rotationTimer -= Time.deltaTime;
            }
        }

        private void EnterCombat()
        {
            ChangeState(EnemyState.InCombat);
        }
    }
}