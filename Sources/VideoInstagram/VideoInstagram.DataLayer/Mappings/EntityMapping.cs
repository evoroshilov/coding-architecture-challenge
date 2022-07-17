using AutoMapper;
using VideoInstagram.DataLayer.Entities;
using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.DataLayer.Mappings
{
    public class EntityMapping : Profile
    {
        public EntityMapping()
        {
            SetupMappings();
        }

        private void SetupMappings()
        {
            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerEntity, Customer>();
        }
    }
}
