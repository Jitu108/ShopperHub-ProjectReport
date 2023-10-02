using Catalog.API.Data.Interface;
using Catalog.API.Data.SqlDataStore.Repo;
using Catalog.API.Data.SqlDataStore;
using Catalog.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Catalog.API.ProtoService;
using Catalog.API.InterServiceCommunication.SyncDataService;
using Catalog.API.InterServiceCommunication.SyncDataClient;

//namespace Catalog.API
//{
//    public class Program
//    {
//        public static async Task Main(string[] args)
//        {
var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<ICatalogBrandRepo, SqlCatalogBrandRepo>();
builder.Services.AddScoped<ICatalogTypeRepo, SqlCatalogTypeRepo>();
builder.Services.AddScoped<IProductRepo, SqlProductRepo>();

builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();
builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IDiscountProductClient, GrpcDiscountProductClient>();

//builder.Services.AddScoped<IDiscountDataClient, GrpcDiscountDataClient>();

var connectionString = Configuration.GetConnectionString("CatalogConn");
// Add DB Context
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(connectionString)
//options.UseInMemoryDatabase("CatalogAPIDb")
);

// Configure Redis Cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = Configuration.GetSection("RedisSettings").GetValue<string>("Configuration"); //"localhost:6379";
    options.InstanceName = Configuration.GetSection("RedisSettings").GetValue<string>("InstanceName"); //"CatalogAPIRedis";
});

// Add GRPC
builder.Services.AddGrpc();

// Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//var host = app.Services.CreateScope

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Seed Data
        var context = services.GetRequiredService<CatalogDbContext>();
        var seeder = new DatabaseSeeder();
        await seeder.SeedAsync(context);

        // Update Discount With New Products
        //var discountDataClient = services.GetRequiredService<IDiscountDataClient>();
        //await discountDataClient.UpdateProductDiscountAsync();
    }
    catch (Exception ex)
    {
        //var logger = services.GetRequiredService<ILogger<Program>>();
        //logger.LogError(ex, $"An error occurred while seeding the database : {ex.Message}");
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
    endpoints.MapGrpcService<GrpcCatalogProductService>();
    endpoints.MapGrpcService<GrpcCatalogBrandService>();
    endpoints.MapGrpcService<GrpcCatalogTypeService>();

    endpoints.MapGet("/protos/GgrpcCatalogProductProvider.proto", async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("ProtoService/GgrpcCatalogProductProvider.proto"));
    });
});

app.Run();
//        }
//    }
//}