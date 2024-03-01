using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ;
using RabbitMQ.Client;
using UserBusinessLogicLayer;
using UserBusinessLogicLayer.PasswordServices;
using UserBusinessLogicLayer.RabbitServices;
using UserBusinessLogicLayer.TokenValidationServices;
using UserDataAccessLayer.Entities;
using UserDataAccessLayer.InternalServices;
using UserDataAccessLayer.UserAppDbContext;
using UserDataAccessLayer.UserAppRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserAppDbContextPostgre>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure MassTransit for publishing messages
/*builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost:5672", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.Publish<UserMessage>(p =>
        {
            p.ExchangeType = ExchangeType.Fanout;
            
        });

        cfg.ReceiveEndpoint("new-user-queue", ep =>
        {
            ep.Bind<UserMessage>();
        });
    });
});*/






builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IUserMapper, UserMapper>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IMessageBusClient, MessageBusClient>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
