using Localization.Test.Infrastructure.Models;
using Localization.Test.Repository.DbContexts;
using Localization.Test.Repository.Interfaces;

namespace Localization.Test.Repository
{
    public class TemplateRepository: GenericRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(LocalizationDbContext context) : base(context)
        {

        }
    }
}
