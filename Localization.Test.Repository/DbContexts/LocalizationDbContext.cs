using Localization.Test.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Localization.Test.Repository.DbContexts
{
    public class LocalizationDbContext : DbContext
    {
        public LocalizationDbContext(DbContextOptions options) : base(options)
        {

        }
        DbSet<User> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Template> Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                        .HasOne(account => account.User)
                        .WithOne(user => user.Account)
                        .HasForeignKey<Account>(account => account.UserId);
            modelBuilder.Entity<Account>()
                        .HasOne(account => account.Creator)
                        .WithMany(user => user.AccountCreators)
                        .HasForeignKey(account => account.CreatedBy);
            modelBuilder.Entity<Account>()
                        .HasOne(account => account.Updator)
                        .WithMany(user => user.AccountUpdators)
                        .HasForeignKey(account => account.UpdatedBy);

            modelBuilder.Entity<Role>()
                        .HasOne(role => role.User)
                        .WithMany(user => user.Roles)
                        .HasForeignKey(role => role.UserId);
            modelBuilder.Entity<Role>()
                        .HasOne(role => role.Creator)
                        .WithMany(user => user.RoleCreators)
                        .HasForeignKey(role => role.CreatedBy);
            modelBuilder.Entity<Role>()
                        .HasOne(role => role.Updator)
                        .WithMany(user => user.RoleUpdators)
                        .HasForeignKey(role => role.UpdatedBy);

            modelBuilder.Entity<Template>()
                        .HasOne(template => template.Creator)
                        .WithMany(template => template.TemlateCreators)
                        .HasForeignKey(template => template.CreatedBy);
            modelBuilder.Entity<Template>()
                        .HasOne(template => template.Updator)
                        .WithMany(user => user.TemplateUpdators)
                        .HasForeignKey(template => template.UpdatedBy);

            modelBuilder.Entity<User>()
                      .HasOne(user => user.Creator)
                      .WithMany(user => user.UserCreators)
                      .HasForeignKey(user => user.CreatedBy);
            modelBuilder.Entity<User>()
                        .HasOne(user => user.Updator)
                        .WithMany(user => user.UserUpdators)
                        .HasForeignKey(user => user.UpdatedBy);
        }
    }
}
