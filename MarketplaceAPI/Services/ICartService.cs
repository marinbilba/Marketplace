using System.Threading.Tasks;
using MarketplaceAPI.Model;

namespace MarketplaceAPI.Services
{
    public interface ICartService
    {
        void DeleteProduct(int cartId, int productId);
        Task PlaceOrder(CustomerOrder order);
    }
}