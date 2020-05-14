using System;
using System.Collections.Generic;
using System.Text;
using eShop.Application.Dtos;

namespace eShop.Application.Catalog.Products.Dtos.Public
{
    public class GetProductPagingRequest:PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
