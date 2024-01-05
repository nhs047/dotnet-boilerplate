using Localization.Test.Repository.Interfaces;

namespace Localization.Test.Repository.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IAccountRepository AccountRepository { get; }
        IRoleRepository RoleRepository { get; }
        ITemplateRepository TemplateRepository { get; }
        void Save();
    }
}
