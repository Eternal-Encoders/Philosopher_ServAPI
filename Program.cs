using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Philosopher_ServAPI.Application;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers;
using Philosopher_ServAPI.Helpers.Exceptions;
using Philosopher_ServAPI.Infrastructure;
using Philosopher_ServAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "test",
                      policy =>
                      {
                          policy
                          .AllowAnyOrigin()
                          //.WithOrigins(configuration["TestOrigin1"],
                          //    configuration["TestOrigin2"])
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                          //.AllowCredentials();
                      });
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    var serializerOptions = opt.JsonSerializerOptions;
    //serializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    serializerOptions.IgnoreReadOnlyProperties = false;
    serializerOptions.WriteIndented = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(config =>
{
    config.AddMaps(typeof(Program).Assembly);
});

//builder.Services.AddDbContext<SQLDBContext>(
//    options =>
//    {
//        options.UseNpgsql(configuration.GetConnectionString("Postgres"));
//    });

builder.Services.AddDbContext<SqlDbContext>(
    options =>
    {
        options.UseSqlite(configuration.GetConnectionString("SQLite"));
    });

// Репозитории

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ILevelRepository, LevelRepository>();
builder.Services.AddScoped<ITextSectionRepository, TextSectionRepository>();

// Сервисы

builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<LevelService>();
builder.Services.AddScoped<TextSectionService>();
builder.Services.AddScoped<TextService>();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("test");

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        var err = context.Features.Get<IExceptionHandlerFeature>().Error;
        context.Response.ContentType = "application/json";

        if (err is AlreadyExistsException existsException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(existsException.Message); 
            return;
        }
        else if (err is NotFoundException notFoundException)
        {
            //await context.Handler404ExceptionAsync(notFoundException);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundException.Message);
            return;
        }

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync(err.Message);
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
