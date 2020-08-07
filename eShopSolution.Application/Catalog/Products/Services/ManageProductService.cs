using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using eShopSolution.Application.Common;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace eShopSolution.Application.Catalog.Products.Services
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;

        public ManageProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name =  request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = request.Name + "_" + DateTime.Now,
                        DateCreated = DateTime.Now,
                        IsDefault = true,
                        SortOrder = 0,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await SaveFileAsync(request.ThumbnailImage)
                    }
                };
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopException($"Can't found productId: {productId}");
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            Console.WriteLine("start funtion");
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        select new { p, pt };
            var data = await query.Skip(0).Take(10).Select(x => new ProductViewModel()
                    {
                        Id = x.p.Id,
                        Price = x.p.Price,
                        OriginalPrice = x.p.OriginalPrice,
                        Stock = x.p.Stock,
                        ViewCount = x.p.ViewCount,
                        DateCreated = x.p.DateCreated,
                        Name = x.pt.Name,
                        Description = x.pt.Description,
                        Details = x.pt.Details,
                        SeoDescription = x.pt.SeoDescription,
                        SeoTitle = x.pt.SeoTitle,
                        SeoAlias = x.pt.SeoAlias,
                        LanguageId = x.pt.LanguageId
                    }).ToListAsync();
            Console.WriteLine("end funtion");
            return data;
        }

        public List<Product> GetAll2()
        {
            return _context.Products.ToList();
        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranslation = await _context.ProductTranslations
                .FirstOrDefaultAsync(x => x.LanguageId == languageId 
                                     && x.ProductId == productId);
            if (product == null)
            {
                return ProductViewModel.GetEmpty();
            }

            var productVm = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = productTranslation?.Description,
                LanguageId = productTranslation?.LanguageId,
                Details = productTranslation?.Details,
                Name = productTranslation?.Name,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation?.SeoAlias,
                SeoDescription = productTranslation?.SeoDescription,
                SeoTitle = productTranslation?.SeoTitle,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productVm;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Price = x.p.Price,
                    OriginalPrice = x.p.OriginalPrice,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    DateCreated = x.p.DateCreated,
                    Name = x.pt.Name,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    SeoAlias = x.pt.SeoAlias,
                    LanguageId = x.pt.LanguageId
                }).ToListAsync();
            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalItems = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslation = await _context.ProductTranslations
                .FirstOrDefaultAsync(x => x.ProductId == request.Id 
                                    && x.LanguageId == request.LanguageId);
            if (product == null || productTranslation == null) 
            {
                throw new EShopException($"Can't found product by Id: {request.Id}");
            }
            productTranslation.Name = request.Name;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.SeoAlias = request.SeoAlias;
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> updatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) 
            {
                throw new EShopException($"Can't found product by Id: {productId}");
            }
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> updateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopException($"Can't found product by Id: {productId}");
            }
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                .FileName.Trim();
            var fileName = Guid.NewGuid().ToString() + originalFileName.ToString();
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
