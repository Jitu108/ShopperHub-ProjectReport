using AutoMapper;
using Basket.API.Data;
using Basket.API.Data.Entities;
using Basket.API.ProtoService;
using Grpc.Core;

namespace Basket.API.InterServiceCommunication.SyncDataService
{
    public class GrpcBasketService : GrpcBasketProvider.GrpcBasketProviderBase
    {
        private readonly IShoppingCartRepo cartRepo;
        private readonly IMapper mapper;

        public GrpcBasketService(IShoppingCartRepo cartRepo, IMapper mapper)
        {
            this.cartRepo = cartRepo;
            this.mapper = mapper;
        }

        public override async Task<GrpcShoppingCart> GrpcGetBasket(GrpcGetBasketRequest request, ServerCallContext context)
        {
            var cartFromDb = await cartRepo.GetBasket(request.UserId);

            if (cartFromDb == null) cartFromDb = new ShoppingCart(request.UserId);

            var cartItems = mapper.Map<List<GrpcShoppingCartItem>>(cartFromDb.Items);

            var cart = mapper.Map<GrpcShoppingCart>(cartFromDb);
            //cartItems.ForEach(x => cart.Items.Add(x));
            
            return cart;
        }

        public override async Task<GrpcBasketBool> GrpcUpdateBasket(GrpcShoppingCart request, ServerCallContext context)
        {
            var cart = mapper.Map<ShoppingCart>(request);
            var status = await cartRepo.UpdateBasket(cart);
            var grpcStatus = new GrpcBasketBool { Response = status };
            return grpcStatus;
        }

        public override async Task<GrpcBasketBool> GrpcDeleteBasket(GrpcDeleteBasketRequest request, ServerCallContext context)
        {
            var status = await cartRepo.DeleteBasket(request.UserId);

            var grpcStatus = new GrpcBasketBool { Response = status };
            return grpcStatus;
        }
    }
}
