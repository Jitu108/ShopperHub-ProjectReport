using AutoMapper;
using Catalog.API.Dtos;
using DiscountAPI.ProtoService;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Polly;

namespace Catalog.API.InterServiceCommunication.SyncDataClient
{
    public class GrpcDiscountProductClient : IDiscountProductClient
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<GrpcDiscountProductClient> logger;

        public GrpcDiscountProductClient(
            IMapper mapper,
            IConfiguration configuration,
            ILogger<GrpcDiscountProductClient> logger)
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }
        public async Task<bool> AddProductAsync(ProductDiscount product)
        {
            bool status = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Product From Discount Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:DiscountService"]);
            var client = new GrpcDiscountProductProvider.GrpcDiscountProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcProductDiscount>(product);
                    var response = await client.GrpcAddDiscountProductAsync(request);
                    status = response.Response;

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not add Brand to Catalog", ex.Message);
            }

            return status;
        }

        public async Task<bool> UpdateProductAsync(ProductDiscount product)
        {
            bool status = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Product From Discount Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:DiscountService"]);
            var client = new GrpcDiscountProductProvider.GrpcDiscountProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcProductDiscount>(product);
                    var response = await client.GrpcUpdateDiscountProductAsync(request);
                    status = response.Response;

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not add Brand to Catalog", ex.Message);
            }

            return status;
        }
    }
}
