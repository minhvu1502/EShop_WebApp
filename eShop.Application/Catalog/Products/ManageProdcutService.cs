using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.Dtos;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;

namespace eShop.Application.Catalog.Products
{
    class ManageProdcutService :IManagerProductService
    {
        private readonly EShopDbContext _context;
        public ManageProdcutService(EShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price =  request.Price,
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public Task<int> Update(ProductEditRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int ProductID)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagedingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
