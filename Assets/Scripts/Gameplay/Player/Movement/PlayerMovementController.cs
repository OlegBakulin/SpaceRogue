using Abstracts;
using Scriptables.Modules;
using UI.Game;
using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementController : BaseController
    {
        private readonly SubscribedProperty<float> _horizontalInput;
        private readonly SubscribedProperty<float> _verticalInput;

        private readonly PlayerSpeedometerView _speedometerView;
        private readonly PlayerMovementModel _model;
        private readonly PlayerView _view;
        


        public PlayerMovementController(
            SubscribedProperty<float> horizontalInput, 
            SubscribedProperty<float> verticalInput,
            EngineModuleConfig config,
            PlayerView view)
        {
            _horizontalInput = horizontalInput;
            _verticalInput = verticalInput;
            _view = view;
            _model = new PlayerMovementModel(config);
            _speedometerView = GameUIController.PlayerSpeedometerView;
            _speedometerView.Init(GetSpeedometerTextValue(0.0f, _model.MaxSpeed));

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
            UpdateSpeedometerValue(currentSpeed, _model.MaxSpeed);
            if (currentSpeed != 0)
            {
                var transform = _view.transform;
                var forwardDirection = transform.TransformDirection(Vector3.up);
                transform.position += forwardDirection * currentSpeed * Time.deltaTime;
            }

            if (newInputValue == 0 && currentSpeed is < 0.1f and > -0.1f)
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

        private void UpdateSpeedometerValue(float currentSpeed, float maxSpeed)
        {
            _speedometerView.UpdateText(GetSpeedometerTextValue(currentSpeed, maxSpeed));
        }

        private static string GetSpeedometerTextValue(float currentSpeed, float maximumSpeed) =>
            currentSpeed switch
            {
                < 0 => "R",
                _ => $"SPD: {Mathf.RoundToInt(currentSpeed/maximumSpeed * 100)}"
            };
    }
}