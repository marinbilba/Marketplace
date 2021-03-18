namespace MarketplaceAPP.Model
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}