using SendGrid;
using SendGrid.Helpers.Mail;
using VideoInstagram.EmailService.Models;

namespace VideoInstagram.EmailService.Services
{
    public class SendGridEmailSender : IEmailSender
    {


        public async Task SendEmailAsync(Email email)
        {
            var client = new SendGridClient("Key from settings");
            var sender = email.Sender;
            var recipients = email.Recipients;
            var ccrecipients = email.CcRecipients;
            var bccRecipients = email.BccRecipients;

            var message = email.Body;

            var response = await client.SendEmailAsync(new SendGridMessage()).ConfigureAwait(false);
            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                var errorText = await response.Body.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception("Failed to send message to SendGrid: " + errorText);
            }
        }
    }
}
