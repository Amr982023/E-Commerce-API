using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.ProductCategoryDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using Mapster;

namespace E_commerce_Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _uow;

        public ProductCategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get parent categories (ParentCategoryId == null)
        public async Task<IEnumerable<CategoryDto>> GetParentsAsync()
        {
            var parents = await _uow.ProductCategories.GetParentCategoriesAsync();
            return parents.Adapt<IEnumerable<CategoryDto>>();
        }

        // Get children of a parent
        public async Task<IEnumerable<CategoryDto>> GetChildrenAsync(int parentId)
        {
            var children = await _uow.ProductCategories.GetChildrenAsync(parentId);
            return children.Adapt<IEnumerable<CategoryDto>>();
        }

        // Check if category has subcategories
        public Task<bool> HasChildrenAsync(int categoryId)
        {
            return _uow.ProductCategories.HasChildrenAsync(categoryId);
        }

        // Check if category contains products
        public Task<bool> HasProductsAsync(int categoryId)
        {
            return _uow.ProductCategories.HasProductsAsync(categoryId);
        }

        // Category search
        public async Task<IEnumerable<CategoryDto>> SearchAsync(string term)
        {
            var results = await _uow.ProductCategories.SearchAsync(term);
            return results.Adapt<IEnumerable<CategoryDto>>();
        }

        // Build category tree
        public async Task<IEnumerable<ProductCategoryTreeDto>> GetCategoryTreeAsync()
        {
            // 1) Get all parent categories
            var parents = await _uow.ProductCategories.GetParentCategoriesAsync();
            var list = parents.Adapt<List<ProductCategoryTreeDto>>();

            // 2) Fill children recursively
            foreach (var parent in list)
            {
                parent.Children = await BuildTree(parent.Id);
            }

            return list;
        }

        private async Task<List<ProductCategoryTreeDto>> BuildTree(int parentId)
        {
            var children = await _uow.ProductCategories.GetChildrenAsync(parentId);

            var list = children.Adapt<List<ProductCategoryTreeDto>>();

            foreach (var child in list)
            {
                child.Children = await BuildTree(child.Id);
            }

            return list;
        }
    }

}
