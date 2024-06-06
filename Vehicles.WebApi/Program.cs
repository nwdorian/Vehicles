using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterAutoFacModules());


// Add services to the container.

string pathToAssemblies = @"C:\Users\Dorian\Documents\GitHub\Vehicles\Vehicles.WebApi\bin\Debug\net8.0";
var allFiles = Directory.GetFiles(pathToAssemblies, "*.dll");
var assemblies = allFiles.Select(file => Assembly.LoadFrom(file)).ToArray();

foreach (var assembly in assemblies)
{
    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterAssemblyModules(assembly));
}



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

app.Run();
