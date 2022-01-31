using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace BuisnessLogic.Utils
{
    public static class EmailUtils
    {
        public static string EmailTemplatesPath { get; set; }
        
        public static void SendEmail(string emailTemplate,string to, Dictionary<string,string> fields)
        {
            var fromAddress = new MailAddress("wisepaymentsinfo@gmail.com", "Info Wise Gateway");
            var toAddress = new MailAddress(to, to);
            const string fromPassword = "Wise2017IL";
            string subject =PrepareSubject("Payment Instruction from <<MERCHANT-NAME>>", fields);
            string body = PrepareBody(emailTemplate,fields);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml=true,
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        private static string PrepareSubject(string subject,Dictionary<string, string> fields)
        {
            foreach(var field in fields)
            {
                subject = subject.Replace("<<" + field.Key + ">>", field.Value);
            }

            return subject;
        }
        private static string PrepareBody(string emailTemplate,Dictionary<string, string> fields)
        {
            string text = GetEmailTemplate(emailTemplate);


            foreach (var field in fields)
            {
                text = text.Replace("<<" + field.Key + ">>", field.Value);
            }

            return text;
        }
        private static string GetEmailTemplate(string emailTemplate)
        {
            return File.ReadAllText(EmailTemplatesPath + emailTemplate);
        }
    }
}
