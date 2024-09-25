using System.Net.Mail;
using System.Net;


namespace lib_utilidades
{
    public class EmailHelper
    {
        public static bool SendEmail(string userEmail, string userPassword, string messageTo, 
            string cc, string subject, string messageBody, int port = 587, string hostEmail = "smtp.gmail.com",
            bool enableSsl = true)
        {
            if (string.IsNullOrEmpty(userEmail))
                throw new ArgumentException("The email user is null or empty");

            if (string.IsNullOrEmpty(userPassword))
                throw new ArgumentException("The user password is null or empty");

            if (string.IsNullOrEmpty(hostEmail))
                throw new ArgumentException("The host is null or empty");

            if (string.IsNullOrEmpty(messageTo))
                throw new ArgumentException("The email to send is null or empty");

            subject = subject == null ? string.Empty : subject;
            messageBody = messageBody == null ? string.Empty : messageBody;

            var mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(messageTo));
            mailMessage.From = new MailAddress(userEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = messageBody;
            mailMessage.IsBodyHtml = true; 

            if (cc != null)
            {
                var split = cc.Split(';');
                foreach (var item in split)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    mailMessage.CC.Add(new MailAddress(item));
                }
            }

            var smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(userEmail, userPassword);
            smtpClient.Host = hostEmail;
            smtpClient.Port = port;
            smtpClient.EnableSsl = enableSsl;
            smtpClient.Send(mailMessage);
            smtpClient = null;
            mailMessage = null;
            return true;
        }
    }
}
