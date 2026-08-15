using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ChatSystem.BLL.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("sabriyazan2@gmail.com", "tyws imrw uzzk vdhm")
            };

            return client.SendMailAsync(
                new MailMessage(from: "sabriyazan2@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                {IsBodyHtml = true});
        }
    }
}
