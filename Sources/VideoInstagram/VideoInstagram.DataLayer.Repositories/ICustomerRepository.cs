using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.DataLayer.Repositories
{
    public interface ICustomerRepository
    {
        Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
        Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken);
        Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken);
        Task RemoveCustomerAsync(int id, CancellationToken cancellationToken);
    }
}
