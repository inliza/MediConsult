using MediConsult.Domain.Entities;
using MediConsult.Domain.Primitives;
using MediConsult.Domain.Repositories;
using MediConsult.Infrastructure.Abstractions;

namespace MediConsult.Infrastructure.Repositories;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IAppSettings appSettings) : base(appSettings)
    {
    }
}
