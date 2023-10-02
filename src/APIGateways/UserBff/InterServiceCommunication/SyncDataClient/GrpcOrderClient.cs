using AutoMapper;
using Grpc.Net.Client;
using Ordering.API.ProtoService;
using Polly;
using UserBff.Dtos;
using UserBff.Enums;

namespace UserBff.InterServiceCommunication.SyncDataClient
{
    public class GrpcOrderClient : IOrderClient
    {
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<GrpcCatalogBrandClient> logger;

        public GrpcOrderClient(
            IMapper mapper,
            IConfiguration configuration,
            ILogger<GrpcCatalogBrandClient> logger
            )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }
        public async Task<bool> AddOrder(OrderCreate order)
        {
            var status = false;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Add order to Order Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:OrderService"]);
            var client = new GrpcOrderProvider.GrpcOrderProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcOrderCreate>(order);
                    var orderResponse = await client.GrpcAddOrderAsync(request);
                    status = orderResponse.Response;
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Add order to Order Service", ex.Message);
            }

            return status;
        }

        public async Task<OrderStatusInfo> CancelOrder(CancelOrderDto orderDto)
        {
            OrderStatusInfo status = OrderStatusInfo.RequestCouldNotBeProcessed;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Cancel order from Order Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:OrderService"]);
            var client = new GrpcOrderProvider.GrpcOrderProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = mapper.Map<GrpcCancelOrderDto>(orderDto);
                    var orderResponse = await client.GrpcCancelOrderAsync(request);
                    status = (OrderStatusInfo)Enum.Parse(typeof(OrderStatusInfo), orderResponse.OrderStatus);
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Cancel order from Order Service", ex.Message);
            }

            return status;
        }

        public async Task<List<CancelledOrderDto>> GetCancelledOrders(int userId)
        {
            List<CancelledOrderDto> order = new List<CancelledOrderDto>();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Get Cancelled orders from Order Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:OrderService"]);
            var client = new GrpcOrderProvider.GrpcOrderProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcOrderByUserIdRequest { UserId = userId };
                    var orderResponse = await client.GetCancelledOrdersAsync(request);
                    order = mapper.Map<List<CancelledOrderDto>>(orderResponse.Orders);
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Get Cancelled orders from Order Service", ex.Message);
            }

            return order;
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            OrderDto order = null;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Get Order by Id from Order Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:OrderService"]);
            var client = new GrpcOrderProvider.GrpcOrderProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcOrderByIdRequest { Id = id };
                    var orderResponse = await client.GrpcGetOrderByIdAsync(request);
                    order = mapper.Map<OrderDto>(orderResponse);
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Get Order by Id from Order Service", ex.Message);
            }

            return order;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserId(int userId)
        {
            List< OrderDto> order = new List<OrderDto>();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Get Orders by UserId from Order Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:OrderService"]);
            var client = new GrpcOrderProvider.GrpcOrderProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcOrderByUserIdRequest { UserId = userId };
                    var orderResponse = await client.GrpcGetOrdersByUserIdAsync(request);
                    order = mapper.Map<List<OrderDto>>(orderResponse.Orders);
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Get Orders by UserId from Order Service", ex.Message);
            }

            return order;
        }

        public async Task<List<RefundedOrderDto>> GetRefundedOrders(int userId)
        {
            List<RefundedOrderDto> order = new List<RefundedOrderDto>();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Get Refunded order from Order Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:OrderService"]);
            var client = new GrpcOrderProvider.GrpcOrderProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcOrderByUserIdRequest { UserId = userId };
                    var orderResponse = await client.GetRefundedOrdersAsync(request);
                    order = mapper.Map<List<RefundedOrderDto>>(orderResponse.Orders);
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Get Refunded order from Order Service", ex.Message);
            }

            return order;
        }

        public async Task<OrderStatusInfo> RefundOrder(int orderId)
        {
            OrderStatusInfo status = OrderStatusInfo.RequestCouldNotBeProcessed;

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt =>
                {
                    logger.LogWarning($"=======> Trying to Refund order from Order Service - Request Retry: {retryAttempt}");
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                });

            var channel = GrpcChannel.ForAddress(configuration["GrpcClient:OrderService"]);
            var client = new GrpcOrderProvider.GrpcOrderProviderClient(channel);

            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var request = new GrpcOrderByIdRequest { Id = orderId };
                    var orderResponse = await client.GrpcRefundOrderAsync(request);
                    status = (OrderStatusInfo)Enum.Parse(typeof(OrderStatusInfo), orderResponse.OrderStatus);
                });
            }
            catch (Exception ex)
            {
                logger.LogError($" =======> Could not Refund order from Order Service", ex.Message);
            }

            return status;
        }
    }
}
