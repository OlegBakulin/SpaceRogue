using System;
using System.Collections.Generic;
using System.Linq;
using Scriptables;
using UnityEngine;
using Random = System.Random;

namespace Utilities.Mathematics
{
    public static class RandomPicker
    {
        public static T PickOneElementByWeights<T>(IEnumerable<WeightConfig<T>> weights, Random r)
        {
            var orderedWeights = weights
                .OrderBy(x => x.Weight)
                .ToArray();
            int weightSum = orderedWeights.Sum(x => x.Weight);
            double[] chances = orderedWeights.Select(x => x.Weight / (double)weightSum).ToArray();
            double randomDouble = r.NextDouble();
            
            if (randomDouble < chances[0]) return orderedWeights[0].Config;
            for (int i = 1; i < chances.Length; i++)
            {
                double sum = chances[..(i-1)].Sum();
                if (randomDouble > sum && randomDouble <= sum + chances[i]) return orderedWeights[i].Config;
            }

            return orderedWeights[^1].Config;
        }

        public static bool TakeChance(float chance, Random r)
        {
            double randomDouble = r.NextDouble();
            return randomDouble <= chance;
        }

        public static float PickRandomBetweenTwoValues(float minValue, float maxValue, Random r)
        {
            float difference = maxValue - minValue;
            return (float)Math.Round(r.NextDouble() * difference + minValue, 2);
        }

        public static int PickRandomBetweenTwoValues(int minValue, int maxValue, Random r) => r.Next(minValue, maxValue + 1);

        public static Vector3 PickRandomAngle(int leftAngle, int rightAngle, Random r) => PickRandomBetweenTwoValues(-leftAngle, rightAngle + 1, r).ToVector3();

        public static Vector3 PickRandomAngle(int angle, Random r) => PickRandomAngle(angle, angle, r);
    }
}