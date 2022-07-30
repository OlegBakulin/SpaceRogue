using System;

namespace Gameplay.Health
{
    public interface IDamageableView
    {
        public event Action<float> DamageTaken;
    }
}