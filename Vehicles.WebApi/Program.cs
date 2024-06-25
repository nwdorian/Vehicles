using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Vehicles.WebApi;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.



builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterAutoFacModules());

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
