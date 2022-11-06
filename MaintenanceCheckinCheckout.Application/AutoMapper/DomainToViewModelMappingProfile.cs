using AutoMapper;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Results;
using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cars, CarResult>();
        }
    }
}
