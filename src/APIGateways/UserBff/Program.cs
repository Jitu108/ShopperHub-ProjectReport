using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UserBff.InterServiceCommunication.SyncDataClient;
using UserBff.Models;
using UserBff.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<ICatalogProductClient, GrpcCatalogProductClient>();
builder.Services.AddScoped<ICatalogBrandClient, GrpcCatalogBrandClient>();
builder.Services.AddScoped<ICatalogTypeClient, GrpcCatalogTypeClient>();
builder.Services.AddScoped<IBasketClient, GrpcBasketClient>();
builder.Services.AddScoped<IIdentityClient, GrpcIdentityClient>();
builder.Services.AddScoped<IOrderClient, GrpcOrderClient>();

builder.Services.AddScoped<ICatalogProductService, CatalogProductService>();
builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();
builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


#region Authentication
var appSettingSection = Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingSection);
var appSetings = appSettingSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSetings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false

        };
    });
#endregion

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

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddEndpointsApiExplorer();

#region Swagger Gen
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopperHub", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllers();

app.Run();
