using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoInstagram.DataLayer.Context;
using VideoInstagram.DataLayer.Entities;
using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.DataLayer.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        private readonly IMapper _mapper;

        public UserRepository(IDataContextFactory dataContextFactory, IMapper mapper)
        {
            _dataContextFactory = dataContextFactory;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(User User, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<UserEntity>(User);

            await using var dataContext = _dataContextFactory.GetDataContext();
            await dataContext.Users.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            User.Id = entity.Id;
        }

        public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();
            var User = await dataContext.Users.FindAsync(id, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<User>(User);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();
            var Users = await dataContext.Users.ToListAsync(cancellationToken).ConfigureAwait(false);
            return _mapper.Map<List<User>>(Users);
        }

        public async Task UpdateUserAsync(User User, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<UserEntity>(User);

            await using var dataContext = _dataContextFactory.GetDataContext();
            var toUpdate = await dataContext.Users.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);

            if (toUpdate == null)
                return;

            _mapper.Map(entity, toUpdate);

            await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }


        public async Task RemoveUserAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();

            var User = await dataContext.Users.FindAsync(id, cancellationToken).ConfigureAwait(false);

            dataContext.Users.Remove(User);

            await dataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
