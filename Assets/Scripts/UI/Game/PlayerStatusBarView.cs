using UI.Common;
using UnityEngine;

namespace UI.Game
{
    public class PlayerStatusBarView : MonoBehaviour
    {
        [field: SerializeField] public SliderView HealthSlider { get; private set; }
        [field: SerializeField] public SliderView ShieldSlider { get; private set; }
    }
}
