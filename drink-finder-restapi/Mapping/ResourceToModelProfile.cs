using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Resources;

namespace drink_finder_restapi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile() 
        {
            CreateMap<SaveDrinkResource, Drink>();
        }
    }
}
