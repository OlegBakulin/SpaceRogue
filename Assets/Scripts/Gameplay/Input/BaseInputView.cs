using UnityEngine;
using Utilities.Reactive.SubscriptionProperty;

namespace Gameplay.Input
{
    public abstract class BaseInputView : MonoBehaviour
    {
        private SubscribedProperty<Vector3> _mousePositionInput;
        
        private SubscribedProperty<float> _verticalAxisInput;

        private SubscribedProperty<bool> _primaryFireInput;
        
        public virtual void Init(
            SubscribedProperty<Vector3> mousePositionInput,
            SubscribedProperty<float> verticalMove,
            SubscribedProperty<bool> primaryFireInput)
        {
            _mousePositionInput = mousePositionInput;
            _verticalAxisInput = verticalMove;
            _primaryFireInput = primaryFireInput;
        }

        protected virtual void OnMousePositionInput(Vector3 value)
            => _mousePositionInput.Value = value;

        protected virtual void OnVerticalInput(float value) 
            => _verticalAxisInput.Value = value;

        protected virtual void OnPrimaryFireInput(bool value)
            => _primaryFireInput.Value = value;
    }
}