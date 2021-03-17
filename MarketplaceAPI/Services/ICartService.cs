using System.Threading.Tasks;
using MarketplaceAPI.Model;

namespace MarketplaceAPI.Services
{
    public interface ICartService
    {
        void DeleteProduct(int cartId, int productId);
        void PlaceOrder(CustomerOrder order);
    }
}