using System.Net;
using System.Net.Mail;
using VideoInstagram.EmailService.Models;
using Attachment = VideoInstagram.EmailService.Models.Attachment;

namespace VideoInstagram.EmailService.Services
{
    public class SmtpEmailSender : IEmailSender
    {
       public async Task SendEmailAsync(Email email)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(email.Sender.Address),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true
            };
            FillAddressCollection(mail.To, email.Recipients);
            FillAddressCollection(mail.CC, email.CcRecipients);
            FillAddressCollection(mail.Bcc, email.BccRecipients);

            FillAttachmentCollection(mail.Attachments, email.Attachments);

            var smtpClient = new SmtpClient("settings", 25)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("settings", " settings")
            };

            await smtpClient.SendMailAsync(mail).ConfigureAwait(false);
        }

        private void FillAddressCollection(MailAddressCollection collection, IReadOnlyList<EmailAddress> recipients)
        {
            foreach (var recipient in recipients)
            {
                collection.Add(recipient.Address);
            }
        }

        private void FillAttachmentCollection(AttachmentCollection attachments, IReadOnlyList<Attachment> modelAttachments)
        {
            foreach (var attachment in modelAttachments)
            {
                attachments.Add(new System.Net.Mail.Attachment(attachment.Content, attachment.FileName));
            }
        }
    }
}
