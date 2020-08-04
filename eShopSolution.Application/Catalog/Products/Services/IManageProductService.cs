using System.Collections.Generic;
using System.Threading.Tasks;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Common;

namespace eShopSolution.Application.Catalog.Products.Services
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<bool> updatePrice(int productId, decimal newPrice);
        Task AddViewCount(int productId);
        Task<bool> updateStock(int productId, int addedQuantity);
        Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
    }
}
