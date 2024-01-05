using Localization.Test.Common.Constants;
using Localization.Test.Common.Exceptions;
using Localization.Test.Common.Models;
using Localization.Test.Common.RequestModels;
using Localization.Test.Common.ResponseModels;
using Localization.Test.Infrastructure.Enums;
using Localization.Test.Infrastructure.Models;
using Localization.Test.Repository.UnitOfWork.Interfaces;
using Localization.Test.Service.Interfaces;
using Localization.Test.Service.Managers;

namespace Localization.Test.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LanguageService _languageService;
        public AuthService(IUnitOfWork unitOfWork, LanguageService languageService)
        {
            _unitOfWork = unitOfWork;
            _languageService = languageService;
        }

        public LoginResponse Login(LoginRequest loginRequest)
        {
            var user = _unitOfWork.UserRepository
                .FindQueryable(u => u.Email == loginRequest.Username)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ErrorOutException(_languageService.GetString(MessageCodes.WRONG_CREDENTIALS));
            }

            var hashedPassword = _unitOfWork.AccountRepository
                .FindQueryable(a => a.UserId == user.Id)
                .Select(a => a.Password)
                .FirstOrDefault();

            if (hashedPassword == null)
            {
                throw new ErrorOutException(_languageService.GetString(MessageCodes.WRONG_CREDENTIALS));
            }

            var isVerified = false;

            if (!string.IsNullOrEmpty(loginRequest.Password))
            {
                isVerified = verify(loginRequest.Password, hashedPassword);
            }

            if (!isVerified)
                throw new ErrorOutException(_languageService.GetString(MessageCodes.WRONG_CREDENTIALS));

            return new LoginResponse()
            {
                Token = GetToken(user)
            };
        }

        public string GetToken(User user)
        {
            var userWithRoles = getUserParamsWithRoles(user);
            return GetToken(userWithRoles.Item1, userWithRoles.Item2);
        }

        private bool verify(string password, string hashedPassword)
        {
            return CryptoManager.Verify(password, hashedPassword) ?
                true :
                false;
        }

        private (UserParams, List<string>) getUserParamsWithRoles(User user)
        {
            List<string> scope = new List<string>();

            var roles = _unitOfWork.RoleRepository.FindQueryable(r => r.UserId == user.Id).ToList();

            getAccess(scope, roles);

            return (new UserParams()
            {
                UserId = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Culture = user.Culture

            }, scope);
        }

        private string GetToken(UserParams userParams, List<string> roles)
        {
            var claims = ClaimManager.PrepareClaims(userParams, roles);
            var token = JwtTokenManager.BuildToken(claims);
            return token;
        }

        private void getAccess(List<string> scope, List<Role> roles)
        {
            foreach (int i in Enum.GetValues(typeof(FeatureEnum)))
            {
                var permission = roles.Find(r => r.Feature == i);
                if (permission == null)
                {
                    continue;
                }

                string feature = ((FeatureEnum)permission.Feature).ToString();

                if (permission.ViewAccess)
                {
                    scope.Add(feature + "." + "read");
                }

                if (permission.CreateAccess)
                {
                    scope.Add(feature + "." + "create");
                }

                if (permission.EditAccess)
                {
                    scope.Add(feature + "." + "edit");
                }

                if (permission.DeleteAccess)
                {
                    scope.Add(feature + "." + "delete");
                }
            }
        }
    }
}
