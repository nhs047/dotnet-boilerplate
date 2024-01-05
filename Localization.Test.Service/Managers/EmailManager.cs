using Localization.Test.Common.Constants;
using Localization.Test.Infrastructure.Models;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Localization.Test.Service.Managers
{
    public class EmailSettings
    {
        public string? FromAddress { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class AppSettings
    {
        public string? AppName { get; set; }
        public string? Email { get; set; }
    }
    public class EmailManager
    {
        private static EmailSettings? _emailSettings;

        public static void Configure(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public static bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        public static async Task<bool> SendRegistrationSuccessfulMail(User user, string mailSubject, string mailBody)
        {
            return await SendAsync(
                 user.FullName,
                 user.Email,
                 mailSubject,
                 mailBody);
        }

        private static async Task<bool> SendAsync(string recipientFullname, string recipientEmail, string emailSubject, string emailBody)
        {
            if (_emailSettings != null)
            {
                using (SmtpClient client = new SmtpClient(_emailSettings?.Host, _emailSettings.Port))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    client.EnableSsl = true;

                    // TODO: Quick fix for when email can't send to the permanently deleted user
                    if (!IsValidEmail(recipientEmail))
                    {
                        return false;
                    }

                    //email display name shouldn't contains comma
                    //that's why we escape comma's with backslash character if exist in the display name
                    recipientFullname = recipientFullname.Replace(Keys.Comma, Keys.CommaEscaped);

                    var fromAddress = _emailSettings.FromAddress;

                    var toAddress = $"{recipientFullname} <{recipientEmail}>";

                    MailMessage mailMessage = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = emailSubject,
                        Body = emailBody,
                        IsBodyHtml = true
                    };

                    try
                    {
                        await client.SendMailAsync(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"{Keys.EmailManagerString} - {MethodBase.GetCurrentMethod()?.Name}: " + JsonConvert.SerializeObject(ex));

                        return false;
                    }

                    Log.Information($"{Keys.MailHasSent}");
                    return true;
                }
            }
            else
            {
                Log.Warning($"{Keys.EmailManagerString} - {MethodBase.GetCurrentMethod()?.Name}: {Keys.ConfigureEmailSettingProperly}");
                return false;
            }
        }
    }
}
