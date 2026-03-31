using UnityEngine;

namespace ContainerLoading.Models
{
    [System.Serializable]
    public class PlacementResult
    {
        public ProductData product;
        public Vector3 position;
        public Quaternion rotation;
        public int loadOrder;
        public bool isValid;
        public string invalidReason;
    }
}
