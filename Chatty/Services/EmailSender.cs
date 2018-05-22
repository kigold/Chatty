using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly IEmailService _EmailService;
        public EmailSender(IEmailService emailService)
        {
            _EmailService = emailService;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //return Task.CompletedTask;
            return Execute(subject, message, email);
        }

       public Task Execute(string subject, string message, string email)
        {
            return _EmailService.SendAsync(email, subject, message);
        }
    }
}
