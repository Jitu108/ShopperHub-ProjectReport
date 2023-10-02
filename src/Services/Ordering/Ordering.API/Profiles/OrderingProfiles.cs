using AutoMapper;
using Ordering.API.Data.Entities;
using Ordering.API.Dtos;
using Ordering.API.Enums;
using Ordering.API.ProtoService;

namespace Ordering.API.Profiles
{
    public class OrderingProfiles  :Profile
    {
        public OrderingProfiles()
        {
            CreateMap<OrderCreate, Order>()
                .ForMember(dest => dest.PaymentMode, option => option.MapFrom(src => (PaymentMode)Enum.Parse(typeof(PaymentMode), src.PaymentMode)))
                .ForMember(dest => dest.DeliveryAddress, option => option.Ignore());

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.PaymentMode, option => option.MapFrom(src => src.PaymentMode.ToString()))
                .ForMember(dest => dest.OrderStatus, option => option.MapFrom(src => src.OrderStatus.ToString()));

            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();

            CreateMap<OrderItemCreate, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<CancelOrderDto, CancelledOrder>();
            CreateMap<CancelledOrder, CancelOrderDto>();

            CreateMap<RefundedOrder, RefundedOrderDto>();
            CreateMap<RefundedOrderDto, RefundedOrder>();

            CreateMap<CancelledOrder, CancelledOrderDto>();
            CreateMap<RefundedOrder, RefundedOrderDto>();

            // GRPC
            CreateMap<GrpcOrderCreate, OrderCreate>()
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => (decimal)src.TotalPrice));

            CreateMap<GrpcOrderItemCreate, OrderItemCreate>()
                .ForMember(dest => dest.UnitPrice, option => option.MapFrom(src => (decimal)src.UnitPrice));

            CreateMap<GrpcAddressDto, AddressDto>();

            CreateMap<OrderDto, GrpcOrderDto>()
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => (double)src.TotalPrice))
                .ForMember(dest => dest.OrderDate, option => option.MapFrom(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(src.OrderDate)));

            CreateMap<OrderItemDto, GrpcOrderItemDto>()
                .ForMember(dest => dest.UnitPrice, option => option.MapFrom(src => (double)src.UnitPrice));

            CreateMap<AddressDto, GrpcAddressDto>();

            CreateMap<GrpcCancelOrderDto, CancelOrderDto>();

            CreateMap<RefundedOrderDto, GrpcRefundedOrderDto>()
            .ForMember(dest => dest.RefundDate, option => option.MapFrom(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(src.RefundDate)))
            .ForMember(dest => dest.RefundedAmount, option => option.MapFrom(src => (double)src.RefundedAmount));

            CreateMap<CancelledOrderDto, GrpcCancelledOrderDto>()
                .ForMember(dest => dest.CancellationDate, option => option.MapFrom(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(src.CancellationDate)));

            CreateMap<RefundedOrderDto, GrpcRefundedOrderDto>()
                .ForMember(dest => dest.RefundedAmount, option => option.MapFrom(src => (double)src.RefundedAmount))
                .ForMember(dest => dest.RefundDate, option => option.MapFrom(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTimeOffset(src.RefundDate)));
        }
    }
}
