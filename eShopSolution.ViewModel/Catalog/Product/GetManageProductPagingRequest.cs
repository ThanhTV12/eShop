using System.Collections.Generic;
using eShopSolution.ViewModel.Common;

namespace eShopSolution.ViewModel.Catalog.Product
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
