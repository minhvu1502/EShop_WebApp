using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.Catalog.Products.Dtos.Manage;
using eShop.Application.Catalog.Products.Dtos.Public;
using eShop.Application.Dtos;

namespace eShop.Application.Catalog.Products.Interface
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
