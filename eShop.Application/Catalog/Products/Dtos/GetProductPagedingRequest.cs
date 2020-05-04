using System;
using System.Collections.Generic;
using System.Text;
using eShop.Application.Dtos;

namespace eShop.Application.Catalog.Products.Dtos
{
   public class GetProductPagedingRequest:PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
