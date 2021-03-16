namespace MarketplaceAPI.Model
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string DeliveryAddress { get; set; }
        public int CustomerOrderId { get; set; }

        public CustomerOrder CustomerOrder { get; set; }
    }
}