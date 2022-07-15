using UnityEngine;
using UnityEngine.UI;

namespace UI.Common
{
    [RequireComponent(typeof(Slider))]
    public class SliderView : MonoBehaviour
    {
        private Slider _slider;
        
        public void Init(float minValue, float maxValue, float currentValue)
        {
            _slider = GetComponent<Slider>();
            _slider.minValue = minValue;
            _slider.maxValue = maxValue;
            _slider.value = currentValue;
        }

        public void UpdateValue(float newValue)
        {
            _slider.value = newValue;
        }
    }
}
