using Localization.Test.Common;
using Localization.Test.Common.Constants;
using Localization.Test.Common.Helpers;
using Localization.Test.Infrastructure.Models;
using Localization.Test.Repository.UnitOfWork.Interfaces;
using Localization.Test.Service.Interfaces;
using Localization.Test.Service.Managers;
using Serilog;
using System.Text;

namespace Localization.Test.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LanguageService _languageService;
        private readonly NotificationConfig _notificationConfig;
        public NotificationService(LanguageService languageService, NotificationConfig notificationConfig, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _languageService = languageService;
            _notificationConfig = notificationConfig;
        }
        public async Task SendRegistrationSuccessfulNotification(User user)
        {
            Helper.SetCulture(user.Culture);

            StringBuilder subjectSB, bodySB;

            var template = _unitOfWork.TemplateRepository.FindOne(t =>
                                t.Title == MailCodes.REGISTRATION_SUCCESSFULL &&
                                t.Culture == user.Culture
                            );

            if (template == null)
            {
                Log.Warning(String.Format(
                        _languageService.GetString(MessageCodes.NO_TEMPLATE_FOUND),
                        MailCodes.REGISTRATION_SUCCESSFULL,
                        user.Culture
                        ));
            }
            else
            {
                subjectSB = new StringBuilder(template.TemplateHeader);
                //subjectSB = new StringBuilder(_languageService.GetString(MailCodes.REGISTRATION_SUCCESS_MAIL_SUBJECT));
                subjectSB = subjectSB.Replace(Keys.AppNamePlaceholder, _notificationConfig.AppName);

                bodySB = new StringBuilder(template.TemplateBody);
                //bodySB = new StringBuilder(_languageService.GetString(MailCodes.REGISTRATION_SUCCESS_MAIL_BODY));
                bodySB = bodySB.Replace(Keys.AppNamePlaceholder, _notificationConfig.AppName)
                    .Replace(Keys.FullnamePlaceholder, user.FullName)
                    .Replace(Keys.AppLinkPlaceholder, _notificationConfig.BaseURL)
                    .Replace(Keys.CompanyNamePlaceholder, _notificationConfig.CompanyName);

                await EmailManager.SendRegistrationSuccessfulMail(user, subjectSB.ToString(), bodySB.ToString());
            }
        }
    }
}
