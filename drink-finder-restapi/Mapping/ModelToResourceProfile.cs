using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Resources;

namespace drink_finder_restapi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Drink, DrinkResource>();
            CreateMap<Establishment, EstablishmentResource>();
            CreateMap<City, CityResource>();
            CreateMap<DrinkCategory, DrinkCategoryResource>();
        }
    }
}
