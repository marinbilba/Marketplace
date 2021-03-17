using MarketplaceAPI.Model;

namespace MarketplaceAPP.Model
{
    public class ProductCart
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string CardId { get; set; }
        public Cart Cart { get; set; }
    }
}