using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Philosopher_ServAPI.Application;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers;
using Philosopher_ServAPI.Helpers.Exceptions;
using Philosopher_ServAPI.Infrastructure;
using Philosopher_ServAPI.Infrastructure.Repositories;
using System.Reflection;
using System.Text.Json;

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
builder.Services.AddSwaggerGen(opt =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
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
builder.Services.AddScoped<IGameProgressRepository, GameProgressRepository>();
builder.Services.AddScoped<ILevelEndingRepository, LevelEndingRepository>();

// Сервисы

builder.Services.AddScoped<TextService>();
builder.Services.AddScoped<TextSectionService>();
builder.Services.AddScoped<LevelService>();
builder.Services.AddScoped<LevelEndingService>();
builder.Services.AddScoped<GameProgressService>();
builder.Services.AddScoped<CardService>();

var app = builder.Build();

app.UseRouting();
app.UseCors("test");

app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.MapOpenApi();
//}

app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        var err = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var res = JsonSerializer.Serialize(new {
            details = err?.Message ?? ""
        });
        context.Response.ContentType = "application/json";

        if (err is AlreadyExistsException existsException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(res);
            return;
        }
        else if (err is NotFoundException notFoundException)
        {
            //await context.Handler404ExceptionAsync(notFoundException);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(res);
            return;
        }

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync(res);
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
