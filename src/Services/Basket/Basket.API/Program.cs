using Basket.API.Data;
using Basket.API.InterServiceCommunication.SyncDataService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddScoped<IShoppingCartRepo, ShoppingCartRepo>();

// Register Db Context
var connectionString = Configuration.GetConnectionString("BasketConn");

//builder.Services.AddDbContext<ShoppingCartDbContext>(option =>
//{
//    option.UseInMemoryDatabase("InMem");
//});

builder.Services.AddDbContext<ShoppingCartDbContext>(options =>
    options.UseSqlServer(connectionString)
);
// Add GRPC
builder.Services.AddGrpc();

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Seed Data
        var context = services.GetRequiredService<ShoppingCartDbContext>();
        var seeder = new DatabaseSeeder();
        await seeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
    }
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcBasketService>();
});

app.Run();
