global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using System;
global using System.Linq;
global using System.Net;
using FluentValidation;
using MaintenanceCheckinCheckout.API.Helpers;
using MaintenanceCheckinCheckout.Application.Interfaces.Service.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;
using MaintenanceCheckinCheckout.Infra.IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Configuracoes adicionadas - builder.services
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo() { Title = "API V1", Version = "V1.0" });
    //options.SwaggerDoc("V2", new OpenApiInfo() { Title = "API V2", Version = "V2.0" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds(x => x.FullName);
});

NativeInjector.RegisterServices(builder.Services);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});

//builder.Services.AddApiVersioning(options =>
//{
//    options.DefaultApiVersion = new ApiVersion(1, 0);
//    options.ReportApiVersions = true;
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.ApiVersionReader = ApiVersionReader.Combine(
//        new HeaderApiVersionReader("Api-Version"),
//        new QueryStringApiVersionReader("Api-Version"));
//}).EnableApiVersionBinding();

builder.Services.AddMvc(options =>
{
    //options.Filters.Add(typeof(DomainExceptionFilter));
    options.Filters.Add(typeof(ValidateActionFilterAttribute));
});
#endregion

var app = builder.Build();

#region Configuracoes adicionadas - app
//var versionSet = app.NewApiVersionSet()
//                    .HasApiVersion(1.0)
//                    .ReportApiVersions()
//                    .Build();

app.UseMiddleware(typeof(ApiExceptionMiddleware));
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/V1/swagger.json", "V1.0");
        //options.SwaggerEndpoint($"/swagger/V2/swagger.json", "V2.0");
    });
}

app.UseHttpsRedirection();


#region Endpoints

app.MapGet("/GetAllCars", async (IGetCarsUseCase getCarsUseCase) =>
{
    app.Logger.LogInformation($"Obtendo todos os registros");

    var response = await getCarsUseCase.Execute();

    return response;
});

app.MapPost("/car/registerAsync", async (RegisterCarRequest request, IRegisterCarUseCase registerCar) =>
{
    app.Logger.LogInformation($"Novo registro de carro solicitado", request);

    var response = await registerCar.Execute(request.Description, request.Plate);

    return response;

});


app.MapPost("/car/pickupAsync", async (PickupCarRequest request, IPickUpCarUseCase pickupCar) =>
{
    app.Logger.LogInformation($"Novo registro de carro solicitado", request);

    var response = await pickupCar.Execute(request.CarId, request.RentedBy, request.Latitude, request.Longitude);

    return Results.Ok(response);

});

app.MapDelete("/car/delete/{id}", async (Guid id, IDeleteCarUseCase deleteUsecase) =>
{
    app.Logger.LogInformation($"Remoção do carro {0} solicitado", id);

    await deleteUsecase.Execute(id);

    return Results.Ok();
});


#endregion


app.Run();