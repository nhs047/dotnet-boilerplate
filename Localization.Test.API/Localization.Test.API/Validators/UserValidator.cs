using FluentValidation;
using Localization.Test.Common.Models;

namespace Localization.Test.API.Validators
{
    public class UserValidator<T> : AbstractValidator<T> where T : UserBase
    {
        public UserValidator()
        {

        }

    }
}
