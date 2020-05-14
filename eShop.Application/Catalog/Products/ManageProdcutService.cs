using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using eShop.Application.Catalog.Products.Dtos;
using eShop.Application.Catalog.Products.Dtos.Manage;
using eShop.Application.Dtos;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

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
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                DateCreated = DateTime.Now,
                ViewCount = 0,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        LanguageId = request.LanguageId,
                        SeoTitle = request.SeoTitle
                    }
                }
            };
           await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x =>
                x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranslations == null)
            {
                throw new EShopException($"Cannot find a product with id: {request.Id}");
            }

            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId); // tìm bản ghi trong database;
            if (product == null) throw new  EShopException($"Cannot find a product: {productId}");
            {
                
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId); // tìm bản ghi trong database;
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product: {productId}");
            product.Stock+=addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagedingRequest request)
        {
            // Select join
            var query = from p in _context.Products
                join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                join pic in _context.ProductInCategories on pt.Id equals pic.ProductId
                join c in _context.Categories on pic.CategoryId equals c.Id
                select new
                {
                    p, pt, pic
                };
            // Filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            // Paging
            int totalRow = await query.CountAsync();
            //Skip: dừng lại ở bản ghi
            //Take lấy ra số phần tử
            var data = await query.Skip((request.PageIndex - 1)*request.PageSize).Take(request.PageSize)
                .Select(p=>new ProductViewModel()
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
                Items =data
            };
            return pagedResult;
        }
    }
}
