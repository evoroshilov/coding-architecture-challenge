using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoInstagram.DataLayer.Context;
using VideoInstagram.DataLayer.Entities;
using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.DataLayer.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        private readonly IMapper _mapper;

        public CustomerRepository(IDataContextFactory dataContextFactory, IMapper mapper)
        {
            _dataContextFactory = dataContextFactory;
            _mapper = mapper;
        }

        public async Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<CustomerEntity>(customer);

            await using var dataContext = _dataContextFactory.GetDataContext();
            await dataContext.Customers.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            customer.Id = entity.Id;
        }

        public async Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();
            var customer = await dataContext.Customers.FindAsync(id, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<Customer>(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();
            var customers = await dataContext.Customers.ToListAsync(cancellationToken).ConfigureAwait(false);
            return _mapper.Map<List<Customer>>(customers);
        }

        public async Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<CustomerEntity>(customer);

            await using var dataContext = _dataContextFactory.GetDataContext();
            var toUpdate = await dataContext.Customers.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);

            if (toUpdate == null)
                return;

            _mapper.Map(entity, toUpdate);

            await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }


        public async Task RemoveCustomerAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();

            var customer = await dataContext.Customers.FindAsync(id, cancellationToken).ConfigureAwait(false);

            dataContext.Customers.Remove(customer);

            await dataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
