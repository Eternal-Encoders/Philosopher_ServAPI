using Microsoft.EntityFrameworkCore;
using Philosopher_ServAPI.Infrastructure;

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<PostgresDBContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("Postgres"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
