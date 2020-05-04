using System;
using System.Collections.Generic;
using System.Text;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.Dtos;

namespace eShop.Application.Catalog.Products
{
    class PublicProductService:IPublicProductService
    {
        public PagedResult<ProductViewModel> GetAllByCategoryId(int categoryId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
