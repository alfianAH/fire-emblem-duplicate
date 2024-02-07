using UnityEngine;

namespace Croxxing.Utility
{
    public struct RandomSelection
    {
        private int minValue;
        private int maxValue;
        public float probability;

        public RandomSelection(int minValue, int maxValue, float probability)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.probability = probability;
        }

        public int GetValue() { return Random.Range(minValue, maxValue + 1); }
    }

    public static class CustomRandomizer
    {
        public static int GetRandomValue(params RandomSelection[] selections)
        {
            float rand = Random.value;
            float currentProb = 0;
            foreach (var selection in selections)
            {
                currentProb += selection.probability;
                if (rand <= currentProb)
                    return selection.GetValue();
            }

            //will happen if the input's probabilities sums to less than 1
            //throw error here if that's appropriate
            return -1;
        }
    }
}