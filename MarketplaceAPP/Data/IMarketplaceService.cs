using System.Collections.Generic;
using System.Threading.Tasks;
using MarketplaceAPP.Model;



namespace MarketplaceAPP.Data
{
    public interface IMarketplaceService
    {
        Task<IList<Category>> GetAllCategoriesAsync();
        Task<IList<Product>> GetAllProductsFromCategoryAsync(int categoryId);
        Task AddProductToCartAsync(Product product, string currentUserUsername);
        Task<Customer> LoginUserAsync(Customer customer);
        Task<Cart> GetCustomerCartAsync(Customer currentUser);

        Task DeleteProductFromCart(int productId,int cartId);
        Task PlaceOrderAsync(CustomerOrder customerOrder);
        Task<IList<CustomerOrder>> GetCustomerOrderHistory(string currentUserUsername);
    }
}