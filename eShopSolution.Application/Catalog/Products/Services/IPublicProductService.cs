using System.Threading.Tasks;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Common;

namespace eShopSolution.Application.Catalog.Products.Services
{
    interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
    }
}
