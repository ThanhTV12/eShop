using System.Collections.Generic;
using eShopSolution.ViewModel.Common;

namespace eShopSolution.ViewModel.Catalog.Product
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public List<int> CategoryIds { get; set; }
    }
}
