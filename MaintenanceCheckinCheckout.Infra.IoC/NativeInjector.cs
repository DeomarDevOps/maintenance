using MaintenanceCheckinCheckout.Application.AutoMapper;
using MaintenanceCheckinCheckout.Application.Interfaces;
using MaintenanceCheckinCheckout.Application.Interfaces.Service.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Validation;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;
using MaintenanceCheckinCheckout.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceCheckinCheckout.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //REPOSITORY
            services.AddScoped<ICarReadOnlyRepository, CarRepository>();
            services.AddScoped<ICarWriteOnlyRepository, CarRepository>();

            //SERVICE
            services.AddScoped<IPickUpCarUseCase, PickUpCarUseCase>();
            services.AddScoped<IRegisterCarUseCase, RegisterCarUseCase>();
            services.AddScoped<IGetCarsUseCase, GetCarsUseCase>();
            services.AddScoped<IDeleteCarUseCase, DeleteCarUseCase>();

            services.AddSingleton<InMemoryDbContext>();
            services.AddScoped<IMinimalValidator, MinimalValidator>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}