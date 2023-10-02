using AdminBff.Dtos;
using AutoMapper;
using Catalog.API.ProtoService;
using Grpc.Net.Client;
using Polly;

namespace AdminBff.InterServiceCommunication.SyncDataClient
{
    public class GrpcCatalogTypeClient : ICatalogTypeClient
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<GrpcCatalogBrandClient> logger;

        public GrpcCatalogTypeClient(
            IMapper mapper,
            IConfiguration configuration,
            ILogger<GrpcCatalogBrandClient> logger
            )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }
        public async Task<bool> AddCatalogTypeAsync(CatalogTypeCreate catalogType)
        {
            var status = false;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Add Type From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogTypeProvider.GrpcCatalogTypeProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcCatalogTypeToCreate>(catalogType);
                    var statusResponse = await client.GrpcAddCatalogTypeAsync(request);
                    status = statusResponse.Response;

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not add Type to Catalog", ex.Message);
            }

            return status;
        }

        public async Task<bool> DeleteCatalogTypeAsync(long catalogtypeId)
        {
            var status = false;
            var policy = Policy.
                Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Delete Type From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogTypeProvider.GrpcCatalogTypeProviderClient(channel);
            try
            {

                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcTypeIdRequest();
                    request.Id = catalogtypeId;
                    var statusResponse = await client.GrpcDeleteCatalogTypeAsync(request);
                    status = statusResponse.Response;
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Type Brand to Catalog", ex.Message);
            }
            return status;
        }

        public async Task<CatalogTypeRead> GetCatalogTypeByIdAsync(long catalogtypeId)
        {
            CatalogTypeRead type = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Getting Brands From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogTypeProvider.GrpcCatalogTypeProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcTypeIdRequest();
                    request.Id = catalogtypeId;
                    var response = await client.GrpcGetCatalogTypeByIdAsync(request);
                    type = mapper.Map<CatalogTypeRead>(response);

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Brands from Catalog could not be retrived", ex.Message);
            }

            return type;
        }

        public async Task<IEnumerable<CatalogTypeRead>> GetCatalogTypesAsync()
        {
            List<CatalogTypeRead> types = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Getting Type From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogTypeProvider.GrpcCatalogTypeProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcTypeEmptyRequest();
                    var response = await client.GrpcGetCatalogTypesAsync(request);
                    types = mapper.Map<IEnumerable<CatalogTypeRead>>(response.Types_).ToList();

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Type from Catalog could not be retrived", ex.Message);
            }

            return types;
        }

        public async Task<bool> UpdateCatalogTypeAsync(CatalogTypeUpdate type)
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
            var client = new GrpcCatalogTypeProvider.GrpcCatalogTypeProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcCatalogTypeToUpdate>(type);
                    var statusResponse = await client.GrpcUpdateCatalogTypeAsync(request);
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
