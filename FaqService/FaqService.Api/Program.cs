using FaqService.Dal;
using FaqService.Service;
using FaqService.Service.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("---> Using PostgreSql DB");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("FaqDbConn")));
}

builder.Services.AddScoped<IFaqRepo, FaqRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddAutoMapper(Assembly.Load("FaqService.Domain"));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//if (builder.Environment.IsDevelopment())
//{
//    SeedDatabase();
//}


app.Run();


//void SeedDatabase()
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var faqRepo = scope.ServiceProvider.GetService<IFaqRepo>();
//        new PrepDb(faqRepo).SeedData();
//    }
//}
