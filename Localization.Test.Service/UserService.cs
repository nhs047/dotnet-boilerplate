using Localization.Test.Common.RequestModels;
using Localization.Test.Common.ResponseModels;
using Localization.Test.Infrastructure.Enums;
using Localization.Test.Infrastructure.Models;
using Localization.Test.Repository.UnitOfWork.Interfaces;
using Localization.Test.Service.Interfaces;
using Localization.Test.Service.Managers;

namespace Localization.Test.Service
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public UserService(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public RegistrationResponse Register(RegistrationRequest registrationRequest)
        {
            var user = createUser(registrationRequest);
            createAccount(user.Id, registrationRequest.Password);
            createRoles(user.Id);
            _unitOfWork.Save();
            var ttt = _notificationService.SendRegistrationSuccessfulNotification(user);
            return new RegistrationResponse
            {
                Culture = user.Culture,
                Email = user.Email,  
                Firstname = user.Firstname, 
                Lastname = user.Lastname,
                Id = user.Id,
                MobileNumber = user.MobileNumber,
                Username = user.Username
            };
        }

        public IEnumerable<User> GetUsers(SearchQueryRequest searchQueryRequest)
        {
            return _unitOfWork.UserRepository.GetAllQueryable()
               .Skip(searchQueryRequest.PageSize * searchQueryRequest.PageCount)
               .Take(searchQueryRequest.PageSize)
               .ToList();
        }

        private User createUser(RegistrationRequest registrationRequest)
        {
            var user = new User()
            {
                Firstname = registrationRequest.Firstname ?? string.Empty,
                Lastname = registrationRequest.Lastname ?? string.Empty,
                Email = registrationRequest.Email,
                MobileNumber = registrationRequest.MobileNumber ?? string.Empty,
                Username = registrationRequest.Email,
                Culture = registrationRequest.Culture
            };
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Save();
            return user;
        }

        private void createAccount(int userId, string password)
        {
            var account = new Account()
            {
                UserId = userId,
                Password = CryptoManager.Hash(password)
            };
            _unitOfWork.AccountRepository.Add(account);
        }

        private void createRoles(int userId)
        {
            IEnumerable<Role> role = new List<Role>()
            {
                new Role()
                {
                    UserId = userId,
                    Feature = (int) FeatureEnum.dashboard,
                    ViewAccess = true,
                    CreateAccess = false,
                    EditAccess = false,
                    DeleteAccess = false
                },
                  new Role()
                {
                    UserId = userId,
                    Feature = (int) FeatureEnum.users,
                    CreateAccess = true,
                    DeleteAccess = true,
                    EditAccess = true,
                    ViewAccess = true
                },

            };
            _unitOfWork.RoleRepository.AddRange(role);
        }

        public User? GetUser(int id)
        {
            return _unitOfWork.UserRepository.FindOne(u => u.Id == id);
        }

        public User? GetUserWithRole(int id)
        {
            return _unitOfWork.UserRepository.FindOneWithRoles(id);
        }

        public User? UpdateUser(UpdateUserRequest userRequest)
        {
            var existingUser = _unitOfWork.UserRepository.FindOne(u => u.Id == userRequest.Id);
            if (existingUser == null) return null;

            existingUser.Firstname = userRequest.Firstname ?? string.Empty;
            existingUser.Lastname = userRequest.Lastname ?? string.Empty;
            existingUser.Email = userRequest.Email;
            existingUser.MobileNumber = userRequest.MobileNumber;
            existingUser.Culture = userRequest.Culture;
            
            _unitOfWork.UserRepository.Update(existingUser);
            _unitOfWork.Save();
            return existingUser;
        }
    }
}
