using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;

namespace E_commerce_Infrastructure.Repositories.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IAccount Accounts { get; private set; }
        public IAddress Addresses { get; private set; }
        public ICountry Countries { get; private set; }
        public IOrderLine OrderLines { get; private set; }
        public IOrderStatus OrderStatuses { get; private set; }
        public IPaymentMethod PaymentMethods { get; private set; }
        public IPaymentType PaymentTypes { get; private set; }
        public IProduct Products { get; private set; }
        public IProductCategory ProductCategories { get; private set; }
        public IProductConfiguration ProductConfigurations { get; private set; }
        public IProductItem ProductItems { get; private set; }
        public IPromotion Promotions { get; private set; }
        public IPromotionCategory PromotionCategories { get; private set; }
        public IShippingMethod ShippingMethods { get; private set; }
        public IShopOrder ShopOrders { get; private set; }
        public IShoppingCart ShoppingCarts { get; private set; }
        public IShoppingCartItem ShoppingCartItems { get; private set; }
        public IUserReview UserReviews { get; private set; }
        public IVariation Variations { get; private set; }
        public IVariationOption VariationOptions { get; private set; }
        public IUser Users { get; private set; }

        public UnitOfWork(
        ApplicationDbContext context,
        IAccount accounts,
        IAddress addresses,
        ICountry countries,
        IOrderLine orderLines,
        IOrderStatus orderStatuses,
        IPaymentMethod paymentMethods,
        IPaymentType paymentTypes,
        IProduct products,
        IProductCategory productCategories,
        IProductConfiguration productConfigurations,
        IProductItem productItems,
        IPromotion promotions,
        IPromotionCategory promotionCategories,
        IShippingMethod shippingMethods,
        IShopOrder shopOrders,
        IShoppingCart shoppingCarts,
        IShoppingCartItem shoppingCartItems,
        IUserReview userReviews,
        IVariation variations,
        IVariationOption variationOptions,
        IUser users
    )
        {
            _context = context;
            Accounts = accounts;
            Addresses = addresses;
            Countries = countries;
            OrderLines = orderLines;
            OrderStatuses = orderStatuses;
            PaymentMethods = paymentMethods;
            PaymentTypes = paymentTypes;
            Products = products;
            ProductCategories = productCategories;
            ProductConfigurations = productConfigurations;
            ProductItems = productItems;
            Promotions = promotions;
            PromotionCategories = promotionCategories;
            ShippingMethods = shippingMethods;
            ShopOrders = shopOrders;
            ShoppingCarts = shoppingCarts;
            ShoppingCartItems = shoppingCartItems;
            UserReviews = userReviews;
            Variations = variations;
            VariationOptions = variationOptions;
            Users = users;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
