using Abstracts;
using Gameplay.Movement;
using UI.Game;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Enemy.Movement
{
    public class EnemyMovementController : BaseController
    {
        private readonly SubscribedProperty<float> _horizontalInput;
        private readonly SubscribedProperty<float> _verticalInput;

        private readonly PlayerSpeedometerView _speedometerView;
        private readonly MovementModel _model;
        private readonly UnitView _view;
        
        
        public EnemyMovementController(
            SubscribedProperty<float> horizontalInput, 
            SubscribedProperty<float> verticalInput,
            MovementModel model,
            UnitView view)
        {
            _horizontalInput = horizontalInput;
            _verticalInput = verticalInput;
            _view = view;
            _model = model;

            _horizontalInput.Subscribe(HandleHorizontalInput);
            _verticalInput.Subscribe(HandleVerticalInput);
        }

        protected override void OnDispose()
        {
            _horizontalInput.Unsubscribe(HandleHorizontalInput);
            _verticalInput.Unsubscribe(HandleVerticalInput);
        }

        
        private void HandleVerticalInput(float newInputValue)
        {
            if (newInputValue != 0)
            {
                _model.Accelerate(newInputValue > 0);
            }
            
            float currentSpeed = _model.CurrentSpeed;
            if (currentSpeed != 0)
            {
                var transform = _view.transform;
                var forwardDirection = transform.TransformDirection(Vector3.up);
                transform.position += forwardDirection * currentSpeed * Time.deltaTime;
            }

            if (newInputValue == 0 && currentSpeed < _model.StoppingSpeed && currentSpeed > -_model.StoppingSpeed)
            {
                _model.StopMoving();
            }
        }
        
        private void HandleHorizontalInput(float newInputValue)
        {
            switch (newInputValue)
            {
                case 0:
                    _model.StopTurning();
                    break;
                case < 0:
                    _model.Turn(true);
                    _view.transform.Rotate(Vector3.forward, _model.CurrentTurnRate * newInputValue);
                    break;
                case > 0:
                    _model.Turn(false);
                    _view.transform.Rotate(Vector3.back, _model.CurrentTurnRate * newInputValue);
                    break;
            }
        }
    }
}