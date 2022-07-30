using System;
using Gameplay.Health;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IDamageableView
    {
        public event Action<float> DamageTaken = (float _) => { };
    }
}