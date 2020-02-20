using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Servaces
{
    public class EmailSender : IEmailSender
    {
        public EmailOptions Options { get; set; }
        public EmailSender(IOptions<EmailOptions> emailOptions)
        {
            Options = emailOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var clint = new SendGridClient(sendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@mealim.org", "جمعية معالم المدينة"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            try
            {
                return clint.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
