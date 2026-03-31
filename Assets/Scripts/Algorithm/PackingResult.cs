using System.Collections.Generic;
using ContainerLoading.Models;

namespace ContainerLoading.Algorithm
{
    [System.Serializable]
    public class PackingResult
    {
        public List<PlacementResult> placements;
        public bool allItemsFit;
        public float usedVolumePercentage;
        public float totalWeight;
    }
}
