using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.Catalog.Products.Dtos.Public;
using eShop.Application.Catalog.Products.Interface;
using eShop.Application.Dtos;
using eShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace eShop.Application.Catalog.Products
{
    class PublicProductService : IPublicProductService
    {
        private EShopDbContext _context;
        public PublicProductService(EShopDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request)
        {
            // Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on pt.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new
                        {
                            p,
                            pt,
                            pic
                        };
            if (request.CategoryId.HasValue == true && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }
            // Paging
            int totalRow = await query.CountAsync();
            //Skip: dừng lại ở bản ghi
            //Take lấy ra số phần tử
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(p => new ProductViewModel()
                {
                    Id = p.p.Id,
                    Name = p.pt.Name,
                    DateCreated = p.p.DateCreated,
                    Description = p.pt.Description,
                    Details = p.pt.LanguageId,
                    OriginalPrice = p.p.OriginalPrice,
                    Price = p.p.Price,
                    SeoAlias = p.pt.SeoAlias,
                    SeoDescription = p.pt.SeoDescription,
                    SeoTitle = p.pt.SeoTitle,
                    Stock = p.p.Stock,
                    ViewCount = p.p.ViewCount
                }).ToListAsync();
            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

    }
}
