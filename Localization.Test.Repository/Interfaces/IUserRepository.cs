using Localization.Test.Infrastructure.Models;

namespace Localization.Test.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? FindOneWithRoles(int id);
    }
}
