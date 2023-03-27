using System;
using WebAdvert.HealthChecks;
using WebAdvert.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IAdvertStorageService, DynamoDBAdvertStorage>();
builder.Services.AddTransient<StorageHealthCheck>();

builder.Services.AddControllers();


builder.Services.AddHealthChecks().AddCheck<StorageHealthCheck>("Storage");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigin", policy => policy.WithOrigins("*").AllowAnyHeader());
});
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
app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

