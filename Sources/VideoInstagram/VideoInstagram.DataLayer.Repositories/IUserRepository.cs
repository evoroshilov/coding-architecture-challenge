using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.DataLayer.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user, CancellationToken cancellationToken);
        Task<User> GetUserAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken);
        Task RemoveUserAsync(int id, CancellationToken cancellationToken);
    }
}
