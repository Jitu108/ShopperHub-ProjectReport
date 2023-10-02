using Identity.API.Data;
using Identity.API.InterServiceCommunication.SyncDataService;
using Identity.API.Models;
using Identity.API.ProtoService;
using Identity.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();

// Add DB Context
var connectionString = Configuration.GetConnectionString("IdentityConn");

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Add GRPC
builder.Services.AddGrpc();

// Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//#region Authentication
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
//#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Swagger Gen
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopperHub", Version="v1" });
    
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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Seed Data
        var context = services.GetRequiredService<UserDbContext>();
        var seeder = new DatabaseSeeder();
        await seeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
    }
}
//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<GrpcIdentityService>();

});

app.Run();
