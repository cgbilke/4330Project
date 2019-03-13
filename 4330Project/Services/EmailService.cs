/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

namespace _4330Project.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            var credentialUserName = "hhah1@lsu.edu";
            var sentFrom = "hhah1@lsu.edu";
            var pwd = "Hope&rkatk01200";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Creatte the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            mail.Subject = message.Subject;
            mail.Body = message.Body;

            // Send:
            return client.SendMailAsync(mail);
        }
    }
}*/