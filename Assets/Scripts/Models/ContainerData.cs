using UnityEngine;

namespace ContainerLoading.Models
{
    [System.Serializable]
    public class ContainerData
    {
        public string containerName;
        public float width;
        public float height;
        public float depth;
        public DoorType doorType;
        public float padding = 0.05f;
    }
}
