using System;
using System.Globalization;
using Abstracts;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ResourceManagement;
using Object = UnityEngine.Object;

namespace UI.Game
{
    public class SpeedometerController : BaseController
    {
        private readonly ResourcePath _speedometerPrefabPath = new("Prefabs/Canvas/Game/Speedometer");
        private readonly float _currentSpeed;
        private readonly float _maxSpeed;
        private readonly Text _textSpeedometer;

        public SpeedometerController(Canvas canvas, float currentSpeed, float maxSpeed)
        {
            _currentSpeed = currentSpeed;
            _maxSpeed = maxSpeed;

            _textSpeedometer = ResourceLoader.LoadObject<Text>(_speedometerPrefabPath);
            _textSpeedometer = Object.Instantiate(_textSpeedometer, canvas.transform, true);
            GameObject gameObject;
            RectTransform rectTransformTextSpeedometer = (gameObject = _textSpeedometer.gameObject).GetComponent<RectTransform>();
            rectTransformTextSpeedometer.anchoredPosition = Vector3.zero;
            rectTransformTextSpeedometer.sizeDelta = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;

            EntryPoint.SubscribeToLateUpdate(UpdateSpeedometer);
        }

        private void UpdateSpeedometer()
        {
            _textSpeedometer.text = "SPD: " + GetCurrentSpeedString(_currentSpeed);
        }

        private string GetCurrentSpeedString(float currentSpeed) => 
            currentSpeed < 0 
                ? "R" 
                : Math.Round(_currentSpeed / _maxSpeed * 100).ToString(CultureInfo.InvariantCulture);

        protected override void OnDispose()
        {
            EntryPoint.UnsubscribeFromLateUpdate(UpdateSpeedometer);
        }
    }
}
