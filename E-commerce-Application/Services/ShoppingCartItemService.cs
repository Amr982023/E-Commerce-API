using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.CartItemDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;
using static E_commerce_Application.Mapping.CartItemMapping;

namespace E_commerce_Application.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IUnitOfWork _uow;

        public ShoppingCartItemService(IUnitOfWork uow)
        {
            _uow = uow;
        }
     
        // Get all items for account
        public async Task<IEnumerable<CartItemDto>> GetItemsAsync(int accountId)
        {
            var items = await _uow.ShoppingCartItems.GetItemsAsync(accountId);
            return items.Select(CartItemMapToDto).ToList();
        }

        // Get single item (basic)
        public async Task<CartItemDto?> GetItemAsync(int accountId, int productItemId)
        {
            var item = await _uow.ShoppingCartItems.GetCartItemAsync(accountId, productItemId);
            return item == null ? null : CartItemMapToDto(item);
        }

        // Get single item with details (if repo includes navigations)
        public async Task<ShoppingCartItemWithDetailsDto?> GetItemWithDetailsAsync(int accountId, int productItemId)
        {
            var item = await _uow.ShoppingCartItems.GetItemWithDetailsAsync(accountId, productItemId);
            return item == null ? null : item.Adapt<ShoppingCartItemWithDetailsDto>();
        }

        // Get all items with details (includes ProductItem & Product)
        public async Task<IEnumerable<ShoppingCartItemWithDetailsDto>> GetItemsWithDetailsAsync(int accountId)
        {
            var items = await _uow.ShoppingCartItems.GetItemsWithDetailsAsync(accountId);
            return items.Adapt<IEnumerable<ShoppingCartItemWithDetailsDto>>();
        }

        // Update quantity
        public async Task UpdateItemQuantityAsync(int accountId, int productItemId, int qty)
        {
            await _uow.ShoppingCartItems.UpdateItemQuantityAsync(accountId, productItemId, qty);
            await _uow.CompleteAsync();
        }

        // Item total price
        public Task<decimal> GetItemTotalPriceAsync(int accountId, int productItemId)
        {
            return _uow.ShoppingCartItems.GetItemTotalPriceAsync(accountId, productItemId);
        }
    }

}
