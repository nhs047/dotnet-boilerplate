using Localization.Test.Repository.DbContexts;
using Localization.Test.Repository.Interfaces;
using Localization.Test.Repository.UnitOfWork.Interfaces;

namespace Localization.Test.Repository.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly LocalizationDbContext _context;
        private bool _disposed = false;
        public UnitOfWork(LocalizationDbContext context)
        {
            _context = context;
        }

        private IUserRepository _userRepository = null!;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        private IAccountRepository _accountRepository = null!;
        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_context);
                }
                return _accountRepository;
            }
        }

        private IRoleRepository _roleRepository = null!;
        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_context);
                }
                return _roleRepository;
            }
        }

        private ITemplateRepository _templateRepository = null!;
        public ITemplateRepository TemplateRepository
        {
            get
            {
                if(_templateRepository == null)
                {
                    _templateRepository = new TemplateRepository(_context);
                }
                return _templateRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
