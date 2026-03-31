namespace ContainerLoading.Models
{
    [System.Serializable]
    public class DeliveryOrder
    {
        public ProductData product;
        public int deliverySequence;
        public string routeId;
        public string destination;
    }
}
