using UnityEngine;

namespace ContainerLoading.Models
{
    [System.Serializable]
    public class ProductData
    {
        public string productName;
        public float width;
        public float height;
        public float depth;
        public float weight;
        public RotationConstraint rotationConstraint;
        public bool canBeStackedOn;
        public float maxStackWeight;
        public Color gizmoColor;
    }
}
