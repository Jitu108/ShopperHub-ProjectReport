using AutoMapper;
using Grpc.Core;
using Ordering.API.Dtos;
using Ordering.API.ProtoService;
using Ordering.API.Services;

namespace Ordering.API.InterServiceCommuncation.SyncDataService
{
    public class GrpcOrderService : GrpcOrderProvider.GrpcOrderProviderBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public GrpcOrderService(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        public override async Task<GrpcOrderBool> GrpcAddOrder(GrpcOrderCreate request, ServerCallContext context)
        {
            var orderCreate = mapper.Map<OrderCreate>(request);
            var status = await orderService.AddOrder(orderCreate);
            var grpcResponse = new GrpcOrderBool { Response = status };
            return grpcResponse;
        }

        public override async Task<GrpcOrderDto> GrpcGetOrderById(GrpcOrderByIdRequest request, ServerCallContext context)
        {
            var orderDto = await orderService.GetOrderById(request.Id);
            var response = mapper.Map<GrpcOrderDto>(orderDto);
            return response;
        }

        public override async Task<GrpcOrdersDto> GrpcGetOrdersByUserId(GrpcOrderByUserIdRequest request, ServerCallContext context)
        {
            try
            {
                var orderDto = await orderService.GetOrdersByUserId(request.UserId);
                var grpcOrders = mapper.Map<List<GrpcOrderDto>>(orderDto);
                var response = new GrpcOrdersDto();
                grpcOrders.ForEach(x => response.Orders.Add(x));
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override async Task<GrpcOrderStatusInfo> GrpcCancelOrder(GrpcCancelOrderDto request, ServerCallContext context)
        {
            var cancelledOrderDto = mapper.Map<CancelOrderDto>(request);
            var status = await orderService.CancelOrder(cancelledOrderDto);
            var response = new GrpcOrderStatusInfo { OrderStatus = status.ToString() };
            return response;
        }

        public override async Task<GrpcOrderStatusInfo> GrpcRefundOrder(GrpcOrderByIdRequest request, ServerCallContext context)
        {
            var status = await orderService.RefundOrder(request.Id);
            return new GrpcOrderStatusInfo { OrderStatus = status.ToString() };
        }

        public override async Task<GrpcCancelledOrdersDto> GetCancelledOrders(GrpcOrderByUserIdRequest request, ServerCallContext context)
        {
            var orders = await orderService.GetCancelledOrders(request.UserId);
            var grpcOrders = mapper.Map<List<GrpcCancelledOrderDto>>(orders);
            var response = new GrpcCancelledOrdersDto();
            grpcOrders.ForEach(x => response.Orders.Add(x));
            return response;
        }

        public async override Task<GrpcRefundedOrdersDto> GetRefundedOrders(GrpcOrderByUserIdRequest request, ServerCallContext context)
        {
            var orders = await orderService.GetRefundedOrders(request.UserId);
            var grpcOrders = mapper.Map<List<GrpcRefundedOrderDto>>(orders);
            var response = new GrpcRefundedOrdersDto();
            grpcOrders.ForEach(x => response.Orders.Add(x));
            return response;
        }
    }
}
