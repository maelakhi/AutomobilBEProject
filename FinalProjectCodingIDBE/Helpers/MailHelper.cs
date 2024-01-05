﻿using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace FinalProjectCodingIDBE.Helpers
{
    public class MailHelper
    {
        public static async Task Send(string toName, string toEmail, string subject, string messageText)
        {
            /* configuration */
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("No Reply", "noreply@coding.id"));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;
            message.Importance = MessageImportance.High;
            message.Priority = MessagePriority.Urgent;  
            message.XPriority = XMessagePriority.Highest;
            /* configuration */

            /* masukin message text */
            message.Body = new TextPart("html")
            {
                Text = messageText,
            };
            /* masukin message text */

            /* kirim email nya */
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
<<<<<<< Updated upstream
<<<<<<< HEAD
                await smtp.AuthenticateAsync("email", "privatekey");
=======
                await smtp.AuthenticateAsync("developprz@gmail.com", "fuhqhzmgnhyibdpm");
>>>>>>> 77c76d26b340fa585978c75e6a737d304862f1ac
=======
                await smtp.AuthenticateAsync("maeljoshua882@gmail.com", "dygrgjjzcauebrzl");
>>>>>>> Stashed changes
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            /* kirim email nya */

        }
    }
}
