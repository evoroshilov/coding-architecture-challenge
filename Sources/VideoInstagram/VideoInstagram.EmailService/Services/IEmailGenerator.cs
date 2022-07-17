using VideoInstagram.EmailService.Models;

namespace VideoInstagram.EmailService.Services
{
    public interface IEmailGenerator
    {
        Task<Email> GenerateEmailFromRequestAsync(EmailRequest request, CancellationToken cancellationToken);
    }
}
