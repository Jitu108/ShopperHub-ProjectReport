using UserBff.Dtos;
using AutoMapper;
using DiscountAPI.ProtoService;
using Grpc.Net.Client;
using Polly;

namespace UserBff.InterServiceCommunication.SyncDataClient
{
    public class GrpcDiscountProductClient : IDiscountProductClient
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<GrpcCatalogBrandClient> logger;

        public GrpcDiscountProductClient(
            IMapper mapper,
            IConfiguration configuration,
            ILogger<GrpcCatalogBrandClient> logger
            )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }
        public async Task<IEnumerable<ProductDiscount>> GetProductDiscountsAsync()
        {
            List<ProductDiscount> productdiscounts = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Get Product From Discount Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:DiscountService"]);
            var client = new GrpcDiscountProductProvider.GrpcDiscountProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcDiscountEmptyRequest();
                    var response = await client.GrpcGetProductDiscountsAsync(request);
                    productdiscounts = mapper.Map<IEnumerable<ProductDiscount>>(response.ProductDiscounts).ToList();

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Get Product From Discount Service", ex.Message);
            }

            return productdiscounts;
        }

        public async Task<bool> UpdateProductDiscountAsync(DiscountUpdateDto update)
        {
            bool status = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Update Product From Discount Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:DiscountService"]);
            var client = new GrpcDiscountProductProvider.GrpcDiscountProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcDiscountUpdate>(update);
                    var response = await client.GrpcUpdateProductDiscountAsync(request);
                    status = response.Response;

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Update Product From Discount Service", ex.Message);
            }

            return status;
        }
    }
}
