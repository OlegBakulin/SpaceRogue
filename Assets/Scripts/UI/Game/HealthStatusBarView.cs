using UI.Abstracts;
using UnityEngine;

namespace UI.Game
{
    public class HealthStatusBarView : MonoBehaviour
    {
        [field: SerializeField] public BarView HealthBar { get; private set; }
    }
}