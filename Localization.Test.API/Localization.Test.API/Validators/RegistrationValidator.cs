using FluentValidation;
using Localization.Test.Common.Constants;
using Localization.Test.Common.Helpers;
using Localization.Test.Common.RequestModels;
using Localization.Test.Repository.UnitOfWork.Interfaces;
using Localization.Test.Service;

namespace Localization.Test.API.Validators
{
    public class RegistrationValidator : UserValidator<RegistrationRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegistrationValidator(IUnitOfWork unitOfWork, LanguageService languageService)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(Helper.IsEmail)
                .WithMessage(languageService.GetString(MessageCodes.PLEASE_PROVIDE_VALID_MAIL))
                .Must(IsUniqueEmail)
                .WithMessage(languageService.GetString(MessageCodes.EMAIL_EXISTS));
            RuleFor(x => x.Password)
                .NotEmpty()
                .Must(Helper.IsPasswordCaseMatched)
                .WithMessage(languageService.GetString(MessageCodes.PASSWORD_POLICY_MISMATCHED));
            RuleFor(x => x.Culture)
               .NotEmpty()
               .NotNull()
               .Must(Helper.IsExistInCultureType)
               .WithMessage(languageService.GetString(MessageCodes.PLEASE_INPUT_VALID_CULTURE));
        }

        private bool IsUniqueEmail(string email)
        {
            return _unitOfWork.UserRepository.FindOne(u => u.Email == email) == null;
        }
    }
}
