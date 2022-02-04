using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CityWatch.Server.Services
{
    public interface IEmailService
    {
        bool SendEmail(string to, string subject, string body, bool sendAshtml, List<string> attachments = null);
        Task<bool> SendEmailAsync(string to, string subject, string body, bool sendAshtml, List<string> attachments = null);
    }
    public class EmailService : IEmailService
    {
        internal static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        private IConfiguration Configuration;

        private string Server;
        private int ServerPort;
        private bool UseSSL;
        private string Sender;
        private string SenderUsername;
        private string SenderPassword;

        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;

            Server = Configuration.GetValue<string>("EmailServer");
            ServerPort = Configuration.GetValue<int>("EmailServerPort");
            UseSSL = Configuration.GetValue<bool>("EmailUseSSL");
            Sender = Configuration.GetValue<string>("EmailSender");
            SenderUsername = Configuration.GetValue<string>("EmailSenderUsername");
            SenderPassword = Configuration.GetValue<string>("EmailSenderPassword");
        }

        public bool SendEmail(string sendTo, string title, string body, bool sendAsHtml, List<string> attachments = null)
        {
            log.Info("Sending email to " + sendTo);

            try
            {
                var smtpClient = new SmtpClient(Server)
                {
                    Port = ServerPort,
                    Credentials = new NetworkCredential(SenderUsername, SenderPassword),
                    EnableSsl = UseSSL,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(Sender),
                    Subject = title,
                    Body = body,
                    IsBodyHtml = sendAsHtml,
                };

                mailMessage.To.Add(sendTo);

                if (attachments != null)
                {
                    foreach (var attach in attachments)
                    {
                        var attachment = new Attachment(attach);
                        mailMessage.Attachments.Add(attachment);
                    }
                }

                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error sending email to: " + sendTo);

                return false;
            }
        }

        public async Task<bool> SendEmailAsync(string sendTo, string title, string body, bool sendAsHtml, List<string> attachments = null)
        {
            log.Info("Sending email to " + sendTo);

            try
            {
                var smtpClient = new SmtpClient(Server)
                {
                    Port = ServerPort,
                    Credentials = new NetworkCredential(SenderUsername, SenderPassword),
                    EnableSsl = UseSSL,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(Sender),
                    Subject = title,
                    Body = body,
                    IsBodyHtml = sendAsHtml,
                };

                mailMessage.To.Add(sendTo);

                if (attachments != null)
                {
                    foreach (var attach in attachments)
                    {
                        var attachment = new Attachment(attach);
                        mailMessage.Attachments.Add(attachment);
                    }
                }

                await Task.Run(() =>
                {
                    smtpClient.SendAsync(mailMessage, null);
                });

                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Error sending email to: " + sendTo);

                return false;
            }
        }
    }
}
