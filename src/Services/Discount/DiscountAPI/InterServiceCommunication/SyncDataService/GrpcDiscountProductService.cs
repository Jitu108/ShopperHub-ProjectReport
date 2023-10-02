using AutoMapper;
using DiscountAPI.Dtos;
using DiscountAPI.ProtoService;
using DiscountAPI.Services;
using Grpc.Core;

namespace DiscountAPI.InterServiceCommunication.SyncDataService
{
    public class GrpcDiscountProductService : GrpcDiscountProductProvider.GrpcDiscountProductProviderBase
    {
        private readonly IDiscountService discountService;
        private readonly IMapper mapper;

        public GrpcDiscountProductService(IDiscountService discountService, IMapper mapper)
        {
            this.discountService = discountService;
            this.mapper = mapper;
        }

        public override async Task<GrpcDiscountBool> GrpcAddDiscountProduct(GrpcProductDiscount request, ServerCallContext context)
        {
            var discountProduct = mapper.Map<ProductDiscount>(request);
            var status = await discountService.AddProductAsync(discountProduct);
            var response = new GrpcDiscountBool();
            response.Response = status;
            return response;
        }

        public async override Task<GrpcDiscountBool> GrpcUpdateDiscountProduct(GrpcProductDiscount request, ServerCallContext context)
        {
            var discountProduct = mapper.Map<ProductDiscount>(request);
            var status = await discountService.UpdateProductAsync(discountProduct);
            var response = new GrpcDiscountBool();
            response.Response = status;
            return response;
        }

        public override async Task<GrpcDiscountBool> GrpcUpdateProductDiscount(GrpcDiscountUpdate request, ServerCallContext context)
        {
            var discountUpdateDto = mapper.Map<DiscountUpdateDto>(request);
            var status = await discountService.UpdateProductDiscountAsync(discountUpdateDto);
            var response = new GrpcDiscountBool();
            response.Response = status;
            return response;
        }

        public override async Task<GrpcProductDiscountList> GrpcGetProductDiscounts(GrpcDiscountEmptyRequest request, ServerCallContext context)
        {
            var discounts = await discountService.GetProductDiscountsAsync();
            var discountList = mapper.Map<List<GrpcProductDiscount>>(discounts);
            var response = new GrpcProductDiscountList();
            discountList.ForEach(x => response.ProductDiscounts.Add(x));
            return response;
        }
    }
}
