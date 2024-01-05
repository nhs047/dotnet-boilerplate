using Localization.Test.Infrastructure.Models;
using Localization.Test.Repository.DbContexts;
using Localization.Test.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Localization.Test.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly LocalizationDbContext _context;
        public UserRepository(LocalizationDbContext context) : base(context) 
        {
            _context = context;
        }

        public User? FindOneWithRoles(int id)
        {
                return _context.Set<User>().Include(x => x.Roles)
                    .AsQueryable()
                    .Where(u => u.Id == id).FirstOrDefault();
        }
    }
}
