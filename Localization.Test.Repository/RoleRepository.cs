using Localization.Test.Infrastructure.Models;
using Localization.Test.Repository.DbContexts;
using Localization.Test.Repository.Interfaces;

namespace Localization.Test.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(LocalizationDbContext context) : base(context) { }
    }
}
