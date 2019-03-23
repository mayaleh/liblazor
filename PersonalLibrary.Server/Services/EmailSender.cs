using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using PersonalLibrary.Server.Models.New;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PersonalLibrary.Server.Services
{
    public class EmailSender : IEmailSender
    {

        public IOptions<AuthMessageSenderOptions> Options { get; }

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor;
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.Value.SendGridKey, subject, message, email);
        }


        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("salim.mayaleh@gmail.com", "Salim Mayaleh"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
