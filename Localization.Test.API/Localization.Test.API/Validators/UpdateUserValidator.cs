using FluentValidation;
using Localization.Test.Common.Constants;
using Localization.Test.Common.Helpers;
using Localization.Test.Common.RequestModels;
using Localization.Test.Repository.UnitOfWork.Interfaces;
using Localization.Test.Service;

namespace Localization.Test.API.Validators
{
    public class UpdateUserValidator : UserValidator<UpdateUserRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserValidator(IUnitOfWork unitOfWork, LanguageService languageService)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(Helper.IsEmail)
                .WithMessage(languageService.GetString(MessageCodes.PLEASE_PROVIDE_VALID_MAIL))
                .Must(IsUniqueEmail)
                .WithMessage(languageService.GetString(MessageCodes.EMAIL_EXISTS));
            RuleFor(x => x.Culture)
               .NotEmpty()
               .NotNull()
               .Must(Helper.IsExistInCultureType)
               .WithMessage(languageService.GetString(MessageCodes.PLEASE_INPUT_VALID_CULTURE));
        }

        private bool IsUniqueEmail(UpdateUserRequest user, string email)
        {
            return _unitOfWork.UserRepository.FindOne(u => u.Email == email && u.Id != user.Id) == null;
        }
    }
}
