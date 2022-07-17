using VideoInstagram.EmailService.Models;

namespace VideoInstagram.EmailService.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Email email);
    }
}
