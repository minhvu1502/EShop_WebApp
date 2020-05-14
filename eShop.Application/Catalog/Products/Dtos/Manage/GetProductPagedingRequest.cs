using System.Collections.Generic;
using eShop.Application.Dtos;

namespace eShop.Application.Catalog.Products.Dtos.Manage
{
   public class GetProductPagedingRequest:PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
