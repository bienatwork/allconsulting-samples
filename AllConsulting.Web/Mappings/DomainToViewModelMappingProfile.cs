using AllConsulting.Entities;
using AllConsulting.Web.Models;
using AutoMapper;

namespace AllConsulting.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public new string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderPosition, OrderPositionViewModel>();
        }
    }
}