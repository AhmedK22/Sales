using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using Application.Interfaces;
using Application.Factories;
using Application.Discounts;
using Application.Decorators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("WebUI",
        policy => policy
            .WithOrigins("http://localhost:5293", "https://localhost:7194")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("SalesDb"));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepository<Domain.Entities.Order>, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<OrderFactory>();
builder.Services.AddScoped<Application.Discounts.DiscountStrategyFactory>();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(Application.Orders.Commands.CreateOrderCommand).Assembly);
});
builder.Services.AddLogging();
builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(Application.Decorators.LoggingDecorator<,>));

var app = builder.Build();



app.UseRouting();
app.UseCors("WebUI");
app.UseAuthorization();
app.MapControllers();
app.Run();
