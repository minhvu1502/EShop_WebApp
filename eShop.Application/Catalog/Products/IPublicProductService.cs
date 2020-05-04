using System;
using System.Collections.Generic;
using System.Text;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.Dtos;

namespace eShop.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        PagedResult<ProductViewModel> GetAllByCategoryId(int categoryId, int pageIndex, int pageSize);
    }
}
