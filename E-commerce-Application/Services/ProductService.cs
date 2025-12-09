using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductDTOs;
using E_commerce_Application.DTOs.ProductItemDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using Mapster;

namespace E_commerce_Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get product by id (simple)
        public async Task<ProductDetailsDto?> GetByIdAsync(int id)
        {
            var product = await _uow.Products.GetByIdAsync(id);
            if (product == null)
                return null;

            return product.Adapt<ProductDetailsDto>();
        }

        // Search
        public async Task<IEnumerable<ProductDto>> SearchAsync(string term, int limit = 20)
        {
            var products = await _uow.Products.SearchProductsAsync(term, limit);

            return products.Adapt<IEnumerable<ProductDto>>();
        }

        // By Category
        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId)
        {
            var products = await _uow.Products.GetProductsByCategoryAsync(categoryId);
            return products.Adapt<IEnumerable<ProductDto>>();
        }

        // Best Selling
        public async Task<IEnumerable<ProductDto>> GetBestSellingAsync(int limit = 10)
        {
            var products = await _uow.Products.GetBestSellingProductsAsync(limit);
            return products.Adapt<IEnumerable<ProductDto>>();
        }

        // Exists?
        public Task<bool> ExistsAsync(int productId)
        {
            return _uow.Products.ProductExistsAsync(productId);
        }

        // Get full details
        public async Task<ProductDetailsDto?> GetProductWithDetailsAsync(int id)
        {
            var product = await _uow.Products.GetProductWithDetails(id);
            if (product == null)
                return null;

            var dto = product.Adapt<ProductDetailsDto>();

            // Fill Category Name if exists
            if (product.Category != null)
                dto.CategoryName = product.Category.CategoryName;

            // MapToReviewDto ProductItems
            if (product.ProductItems != null)
                dto.ProductItems = product.ProductItems.Adapt<IEnumerable<ProductItemDto>>();

            return dto;
        }

    }

}
