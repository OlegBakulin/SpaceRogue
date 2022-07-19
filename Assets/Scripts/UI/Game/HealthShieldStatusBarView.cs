using UI.Abstracts;
using UnityEngine;

namespace UI.Game
{
    public class HealthShieldStatusBarView : HealthStatusBarView
    {
        [field: SerializeField] public BarView ShieldBar { get; private set; }
    }
}