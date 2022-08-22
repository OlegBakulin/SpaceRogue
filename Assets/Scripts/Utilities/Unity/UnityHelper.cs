using UnityEngine;

namespace Utilities.Unity
{
    public static class UnityHelper
    {
        public static bool IsAnyObjectAtPosition(Vector3 position, float radius)
        {
            var collider = Physics2D.OverlapCircle(position, radius);
            return collider is not null;
        }

        public static bool Approximately(float a, float b, float precision) => Mathf.Abs(b - a) <= precision;

        public static bool Approximately(Vector3 original, Vector3 other, float precision) => 
            Approximately(original.x, other.x, precision) && 
            Approximately(original.y, other.y, precision);
    }
}