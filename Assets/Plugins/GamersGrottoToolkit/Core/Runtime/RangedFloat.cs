using System;
using Random = UnityEngine.Random;

namespace GamersGrotto.Core
{
    [Serializable]
    public struct RangedFloat
    {
        public float minValue;
        public float maxValue;

        public float Value => Random.Range(minValue, maxValue);
    }
}