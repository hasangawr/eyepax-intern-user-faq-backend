using AuthenticationBusinessLogicLayer;
using AuthenticationBusinessLogicLayer.PasswordServices;
using AuthenticationDataAccessLayer.AuthentcationAppDbContext;
using AuthenticationDataAccessLayer.AuthenticationRepo;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using AuthenticationBusinessLogicLayer.RabbitMqServices;
using AuthenticationDataAccessLayer.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AuthenticationDbContextPostgre>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IAuthenticationRepo,AuthenticationRepo>();

builder.Services.AddScoped<IAuthenticationBusinessLogic, AuthenticationBusinessLogic>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
