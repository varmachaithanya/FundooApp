using System.Text;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(x =>
    {
        x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
        {
            config.UseHealthCheck(provider);
            config.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
        }));
    });
builder.Services.AddMassTransitHostedService();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option=>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "FundooApp", Version = "v1"
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    }); ;
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
    }
});
});
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserBusiness,UserBusiness>();
builder.Services.AddScoped<INoteRepository,NoteRepository>();
builder.Services.AddScoped<INotesBusiness,NotesBusiness>();
builder.Services.AddScoped<IGetRepository,GetRepository>();
builder.Services.AddScoped<IGetBusiness,GetBusiness>();
builder.Services.AddScoped<ICollabRepository, CollabRepository>();
builder.Services.AddScoped<ICollabBusiness,CollabBusiness>();

builder.Services.AddDbContext<FundoContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        });
builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllers();

app.Run();
