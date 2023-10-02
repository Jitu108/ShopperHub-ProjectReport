using AdminBff.Dtos;
using AutoMapper;
using Catalog.API.ProtoService;
using Grpc.Net.Client;
using Polly;

namespace AdminBff.InterServiceCommunication.SyncDataClient
{
    public class GrpcCatalogBrandClient : ICatalogBrandClient
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<GrpcCatalogBrandClient> logger;

        public GrpcCatalogBrandClient(
            IMapper mapper,
            IConfiguration configuration,
            ILogger<GrpcCatalogBrandClient> logger
            )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }
        public async Task<bool> AddCatalogBrandAsync(CatalogBrandCreate brand)
        {
            var status = false;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Add Brand From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogBrandProvider.GrpcCatalogBrandProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcCatalogBrandToCreate>(brand);
                    var statusResponse = await client.GrpcAddCatalogBrandAsync(request);
                    status = statusResponse.Response;

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not add Brand to Catalog", ex.Message);
            }

            return status;
        }

        public async Task<bool> DeleteCatalogBrandAsync(long brandId)
        {
            var status = false;
            var policy = Policy.
                Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Delete Brand From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogBrandProvider.GrpcCatalogBrandProviderClient(channel);
            try
            {

                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcBrandIdRequest();
                    request.Id = brandId;
                    var statusResponse = await client.GrpcDeleteCatalogBrandAsync(request);
                    status = statusResponse.Response;
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Delete Brand to Catalog", ex.Message);
            }
            return status;
        }

        public async Task<CatalogBrandRead> GetCatalogBrandByIdAsync(long catalogBrandId)
        {
            CatalogBrandRead product = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Getting Brands From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogBrandProvider.GrpcCatalogBrandProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcBrandIdRequest();
                    request.Id = catalogBrandId;
                    var response = await client.GrpcGetCatalogBrandByIdAsync(request);
                    product = mapper.Map<CatalogBrandRead>(response);

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Brands from Catalog could not be retrived", ex.Message);
            }

            return product;
        }

        public async Task<IEnumerable<CatalogBrandRead>> GetCatalogBrandsAsync()
        {
            List<CatalogBrandRead> products = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Getting Brands From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogBrandProvider.GrpcCatalogBrandProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcBrandEmptyRequest();
                    var response = await client.GrpcGetCatalogBrandsAsync(request);
                    products = mapper.Map<IEnumerable<CatalogBrandRead>>(response.Brands).ToList();

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Brand from Catalogs could not be retrived", ex.Message);
            }

            return products;
        }

        public async Task<bool> UpdateCatalogBrandAsync(CatalogBrandUpdate brand)
        {
            var status = false;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Add Brand From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogBrandProvider.GrpcCatalogBrandProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcCatalogBrandToUpdate>(brand);
                    var statusResponse = await client.GrpcUpdateCatalogBrandAsync(request);
                    status = statusResponse.Response;

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
