namespace Abstracts
{
    public interface IBarView
    {
        public void Init(float minValue, float maxValue, float currentValue);
        public void UpdateValue(float newValue);
    }
}