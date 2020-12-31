using Microsoft.Extensions.Configuration;
using SprintReport.Console.Model.Enums;
using SprintReport.Console.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace SprintReport.Console.Business
{
    public class EmailBusiness
    {
        private readonly IConfigurationBuilder _configurationBuilder;

        public EmailBusiness(IConfigurationBuilder configurationBuilder)
            => _configurationBuilder = configurationBuilder;

        #region Private Methods
        
        private string GetHost()
            =>  (EmailHostEnum)int.Parse(_configurationBuilder.Build().GetSection("emailHost").Value) switch
            {
                EmailHostEnum.Gmail => EmailHostEnum.Gmail.GetDescription<EmailHostEnum>(),
                EmailHostEnum.Hotmail => EmailHostEnum.Hotmail.GetDescription<EmailHostEnum>(),
                _ => throw new ArgumentException("Host not found")
            };

        private void GetUserAndPassword(out string username, out string password)
        {
            username = _configurationBuilder.Build().GetSection("emailUserName").Value;
            if(string.IsNullOrEmpty(username))
                throw new ArgumentException("Username not found");
            
            password = _configurationBuilder.Build().GetSection("emailPassword").Value;
            if(string.IsNullOrEmpty(username))
                throw new ArgumentException("Password not found");
        }

        #endregion

        #region Public Methods
        
        public bool Send(out string error, string subject, string msg, IList<string> to, IList<string> cc, IList<string> attachment)
        {
            Attachment PrepareAttachment(string filePath)
            {
                Attachment data = new Attachment(filePath, MediaTypeNames.Application.Octet);
                
                ContentDisposition disposition = data.ContentDisposition;
                
                disposition.CreationDate = System.IO.File.GetCreationTime(filePath);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(filePath);

                if (!File.Exists(filePath))
                {
                    throw new Exception("");
                }

                return data;
            }

            using (MailMessage mail = new MailMessage())
            {
                GetUserAndPassword(out var username, out var password);

                SmtpClient client = new SmtpClient(GetHost())
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(username, password)
                };
                
                // mail.Sender = new MailAddress("email que vai enviar", "ENVIADOR");
                // mail.From = new MailAddress("de quem", "ENVIADOR");

                mail.Sender = new MailAddress(username);
                mail.From = new MailAddress(username);
                
                foreach(var item in to)
                {
                    mail.To.Add(item);
                }
                
                foreach(var item in cc)
                {
                    mail.CC.Add(item);
                }

                foreach(var item in attachment)
                {
                    mail.Attachments.Add(PrepareAttachment(item));
                }
                
                mail.Subject = subject;
                mail.Body = msg;//" Mensagem do site:<br/> Nome:  Teste<br/> Email : Teste@teste.com  <br/> Mensagem : msg"; 
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                try
                {
                    client.Send(mail);

                    error = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
        }

        #endregion
    }
}