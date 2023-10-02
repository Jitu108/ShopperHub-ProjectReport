using DiscountAPI.Data.Interface;
using DiscountAPI.Data.SqlDataStore.Repo;
using DiscountAPI.Data.SqlDataStore;
using DiscountAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DiscountAPI.Data;
using DiscountAPI.InterServiceCommunication.SyncDataClient;
using DiscountAPI.InterServiceCommunication.SyncDataService;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<IDiscountRepo, SqlDiscountRepo>();

builder.Services.AddScoped<IDiscountService, DiscountService>();

builder.Services.AddScoped<ICatalogProductClient, GrpcCatalogProductClient>();

// Add DB Context
builder.Services.AddDbContext<DiscountDbContext>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString("DiscountConn"));
});

// Add GRPC
builder.Services.AddGrpc();

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

var seeder = new DataSeeder();
await seeder.SeedAsyc(app);

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();
app.UseAuthorization();

//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcDiscountProductService>();
});

app.Run();
