using Localization.Test.Infrastructure.Models;
using Localization.Test.Repository.DbContexts;
using Localization.Test.Repository.Interfaces;

namespace Localization.Test.Repository
{
    public class AccountRepository: GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(LocalizationDbContext context) : base(context) { }

    }
}
