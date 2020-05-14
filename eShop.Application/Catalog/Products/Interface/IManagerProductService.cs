using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.Catalog.Products.Dtos.Manage;
using eShop.Application.Dtos;

namespace eShop.Application.Catalog.Products
{
    public interface IManagerProductService
    {
         Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int ProductID);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task  AddViewCount(int productId);

        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task<List<ProductViewModel>> GetAll();

       Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagedingRequest request);
    }
}
