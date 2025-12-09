using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.CartItemDTOs;
using E_commerce_Application.DTOs.ShoppingCartDTOs;
using E_commerce_Application.Services_Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Models;
using Mapster;

namespace E_commerce_Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _uow;

        public ShoppingCartService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Get full cart
        public async Task<CartDto> GetCartAsync(int accountId)
        {
            var cart = await _uow.ShoppingCarts.GetCartWithItemsAsync(accountId);
            return cart.Adapt<CartDto>();
        }

        // Add item to cart
        public async Task AddItemAsync(int accountId, int productItemId, int qty)
        {
            // Get or create cart
            var cart = await _uow.ShoppingCarts.GetCartWithItemsAsync(accountId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    AccountId = accountId,
                    Items = new List<ShoppingCartItem>()
                };

                await _uow.ShoppingCarts.AddAsync(cart);
                await _uow.CompleteAsync(); // generate CartId
            }

            // Use repo method (needs cartId)
            await _uow.ShoppingCarts.AddItemAsync(cart.Id, productItemId, qty);
            await _uow.CompleteAsync();
        }

        // Update quantity of item
        public async Task UpdateItemQuantityAsync(int accountId, int productItemId, int qty)
        {
            await _uow.ShoppingCarts.UpdateItemQuantityAsync(accountId, productItemId, qty);
            await _uow.CompleteAsync();
        }

        // Remove item
        public async Task RemoveItemAsync(int accountId, int productItemId)
        {
            await _uow.ShoppingCarts.RemoveItemAsync(accountId, productItemId);
            await _uow.CompleteAsync();
        }

        // Clear entire cart
        public async Task ClearCartAsync(int accountId)
        {
            await _uow.ShoppingCarts.ClearCartAsync(accountId);
            await _uow.CompleteAsync();
        }

    }

}
