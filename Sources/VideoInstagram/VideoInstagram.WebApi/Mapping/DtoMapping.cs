using AutoMapper;
using VideoInstagram.Shared.Contracts;
using VideoInstagram.WebApi.Dtos;

namespace VideoInstagram.WebApi.Mapping
{
    public class DtoMapping : Profile
    {
        public DtoMapping()
        {
            SetupMappings();
        }

        private void SetupMappings()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
