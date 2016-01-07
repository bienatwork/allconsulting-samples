using AutoMapper;

namespace AllConsulting.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public new string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
        }
    }
}