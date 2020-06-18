using AutoMapper;
using Domain.Services.Models;
using Presentation.WebAPI.Models;

namespace Presentation.WebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<ProductionPlanViewModel, ProductionPlan>().ReverseMap();
            CreateMap<PowerplantOutputPowerViewModel, PowerplantOutputPower>().ReverseMap();
            CreateMap<FuelsViewModel, Fuels>().ReverseMap();
            CreateMap<PowerplantViewModel, Powerplant>().ReverseMap();
        }
    }
}