using UserBff.Dtos;
using AutoMapper;
using Catalog.API.ProtoService;
using Grpc.Net.Client;
using Polly;

namespace UserBff.InterServiceCommunication.SyncDataClient
{
    public class GrpcCatalogProductClient : ICatalogProductClient
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<GrpcCatalogProductClient> logger;

        public GrpcCatalogProductClient(
            IMapper mapper,
            IConfiguration configuration,
            ILogger<GrpcCatalogProductClient> logger
            )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<IEnumerable<ProductRead>> GetAllProductsAsync()
        {
            List<ProductRead> products = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Getting Products From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogProductProvider.GrpcCatalogProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcProductEmptyRequest();
                    var response = await client.GrpcGetAllProductsAsync(request);
                    products = mapper.Map<IEnumerable<ProductRead>>(response.Products).ToList();

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Products from Catalog could not be retrived", ex.Message);
            }

            return products;
        }

        public async Task<IEnumerable<ProductRead>> GetProductByBrandIdAsync(long catalogBrandId)
        {
            List<ProductRead> products = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Getting Products From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogProductProvider.GrpcCatalogProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcProductIdRequest();
                    request.Id = catalogBrandId;
                    var response = await client.GrpcGetProductByBrandIdAsync(request);
                    products = mapper.Map<IEnumerable<ProductRead>>(response.Products).ToList();
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Products from Catalog could not be retrived", ex.Message);
            }

            return products;
        }

        public async Task<IEnumerable<ProductRead>> GetProductByCatalogTypeIdAsync(long catalogtypeId)
        {
            List<ProductRead> products = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Getting Products From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogProductProvider.GrpcCatalogProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcProductIdRequest();
                    request.Id = catalogtypeId;
                    var response = await client.GrpcGetProductByCatalogTypeIdAsync(request);
                    products = mapper.Map<IEnumerable<ProductRead>>(response.Products).ToList();

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Products from Catalog could not be retrived", ex.Message);
            }

            return products;
        }

        public async Task<ProductRead> GetProductByIdAsync(long productId)
        {
            ProductRead product = new();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Getting Products From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogProductProvider.GrpcCatalogProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcProductIdRequest();
                    request.Id = productId;
                    var response = await client.GrpcGetProductByIdAsync(request);
                    product = mapper.Map<ProductRead>(response);

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Products from Catalog could not be retrived", ex.Message);
            }

            return product;
        }

        public async Task<bool> AddProductAsync(ProductCreate productCreateDto)
        {
            var status = false;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Add Product To Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogProductProvider.GrpcCatalogProductProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcCatalogProductToCreate>(productCreateDto);
                    var statusResponse = await client.GrpcAddProductAsync(request);
                    status = statusResponse.Response;

                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not add Product to Catalog", ex.Message);
            }

            return status;
        }

        public async Task<bool> UpdateProductAsync(ProductCreate productCreateDto)
        {
            var status = false;
            var policy = Policy.
                Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Update Product From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogProductProvider.GrpcCatalogProductProviderClient(channel);
            try
            {

                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcCatalogProductToUpdate>(productCreateDto);
                    var statusResponse = await client.GrpcUpdateProductAsync(request);
                    status = statusResponse.Response;
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not update Product to Catalog", ex.Message);
            }
            return status;
        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            var status = false;
            var policy = Policy.
                Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Delete Product From Catalog Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:CatalogService"]);
            var client = new GrpcCatalogProductProvider.GrpcCatalogProductProviderClient(channel);
            try
            {

                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcProductIdRequest();
                    request.Id = productId;
                    var statusResponse = await client.GrpcDeleteProductAsync(request);
                    status = statusResponse.Response;
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Delete Product to Catalog", ex.Message);
            }
            return status;
        }
    }
}
