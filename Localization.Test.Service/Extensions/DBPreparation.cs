using Localization.Test.Repository.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Localization.Test.Service.Extensions
{
    public static class DBPreparation
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<LocalizationDbContext>();
            dataContext.Database.Migrate();
        }
    }
}
