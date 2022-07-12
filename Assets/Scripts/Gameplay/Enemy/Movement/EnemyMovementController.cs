using Abstracts;
using UnityEngine;

namespace Gameplay.Enemy.Movement
{
    public class EnemyMovementController : BaseController
    {
        private readonly EnemyView _view;
        private readonly EnemyMovementModel _model;

        public EnemyMovementController(EnemyView view, EnemyMovementModel model)
        {
            _view = view;
            _model = model;
            EntryPoint.SubscribeToUpdate(HandleMovement);
            EntryPoint.SubscribeToUpdate(HandleTurning);
        }

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromUpdate(HandleMovement);
            EntryPoint.UnsubscribeFromUpdate(HandleTurning);
        }

        private void HandleMovement()
        {
            float accelerationValue = _model.CurrentAcceleration;
            
            if (accelerationValue != 0)
            {
                _model.Accelerate(accelerationValue > 0);
            }
            
            float currentSpeed = _model.CurrentSpeed;
            if (currentSpeed == 0) return;
            
            var transform = _view.transform;
            var forwardDirection = transform.TransformDirection(Vector3.up);
            transform.position += forwardDirection * currentSpeed * Time.deltaTime;
        }
        
        private void HandleTurning()
        {
            float turnMultiplier = _model.CurrentTurnRateMultiplier;
            switch (turnMultiplier)
            {
                case 0:
                    _model.StopTurning();
                    break;
                case < 0:
                    _model.Turn(true);
                    _view.transform.Rotate(Vector3.forward, _model.CurrentTurnRate * turnMultiplier);
                    break;
                case > 0:
                    _model.Turn(false);
                    _view.transform.Rotate(Vector3.back, _model.CurrentTurnRate * turnMultiplier);
                    break;
            }
        }
    }
}