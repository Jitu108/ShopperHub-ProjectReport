﻿using UserBff.Dtos;
using UserBff.InterServiceCommunication.SyncDataClient;

namespace UserBff.Services
{
    public class DiscountProductService : IDiscountProductService
    {
        private readonly IDiscountProductClient discountProductClient;

        public DiscountProductService(IDiscountProductClient discountProductClient)
        {
            this.discountProductClient = discountProductClient;
        }
        public Task<IEnumerable<ProductDiscount>> GetProductDiscountsAsync()
        {
            return discountProductClient.GetProductDiscountsAsync();
        }

        public Task<bool> UpdateProductDiscountAsync(DiscountUpdateDto update)
        {
            return discountProductClient.UpdateProductDiscountAsync(update);
        }
    }
}
