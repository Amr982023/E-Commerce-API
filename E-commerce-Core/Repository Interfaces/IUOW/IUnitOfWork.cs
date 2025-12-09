using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Interfaces.Unit_Of_Work_Interface
{
    public interface IUnitOfWork : IDisposable
    {
        //another interfaces
         IAccount Accounts { get; }
         IAddress Addresses { get; }
         ICountry Countries { get; }
         IOrderLine OrderLines { get; }
         IOrderStatus OrderStatuses { get; }
         IPaymentMethod PaymentMethods { get; }
         IPaymentType PaymentTypes { get; }
         IProduct Products { get; }
         IProductCategory ProductCategories { get; }
         IProductConfiguration ProductConfigurations { get; }
         IProductItem ProductItems { get; }
         IPromotion Promotions { get; }
         IPromotionCategory PromotionCategories { get; }
         IShippingMethod ShippingMethods { get;}
         IShopOrder ShopOrders { get;}
         IShoppingCart ShoppingCarts { get; }
         IShoppingCartItem ShoppingCartItems { get; }
         IUserReview UserReviews { get; }
         IVariation Variations { get; }
         IVariationOption VariationOptions { get; }
         IUser Users { get; }

        int Complete(); 
        Task<int> CompleteAsync();
    }
}
