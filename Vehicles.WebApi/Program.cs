using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;
using Vehicles.WebApi;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

string pathToAssemblies = @"C:\Users\Dorian\Documents\GitHub\Vehicles\Vehicles.WebApi\bin\Debug\net8.0";
var allFiles = Directory.GetFiles(pathToAssemblies, "*.dll");
var assemblies = allFiles.Select(file => Assembly.LoadFrom(file)).ToArray();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        foreach (var assembly in assemblies)
        {
            builder.RegisterAssemblyModules(assembly);
        }
    });

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<VehicleProfile>();
});

builder.Services.AddAutoMapper(typeof(VehicleProfile));

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

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
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
