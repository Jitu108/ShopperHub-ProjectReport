using AutoMapper;
using Catalog.API.ProtoService;
using DiscountAPI.Dtos;
using DiscountAPI.Services;
using Grpc.Net.Client;
using Polly;

namespace DiscountAPI.InterServiceCommunication.SyncDataClient
{
    public class GrpcCatalogProductClient : ICatalogProductClient
    {
        private readonly IDiscountService discountService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<GrpcCatalogProductClient> logger;

        public GrpcCatalogProductClient(
            IDiscountService discountService,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<GrpcCatalogProductClient> logger
            )
        {
            this.discountService = discountService;
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }
        public async Task AddProductsFromCatalogAsync()
        {
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Product Refresh From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogProductProvider.GrpcCatalogProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcEmptyRequest();
                    var response = await client.GrpcGetAllProductsLeanAsync(request);
                    var products = mapper.Map<IEnumerable<ProductRead>>(response.Products);

                    await discountService.AddProductsAsync(products.ToList());

                });
            }
            catch(Exception ex)
            {
                logger.LogError($" =======> Products from Catalog could not be retrived", ex.Message);
            }
        }
    }
}
