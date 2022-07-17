namespace VideoInstagram.EmailService.Models
{
    public class Email
    {
        public IReadOnlyList<Attachment> Attachments { get; set; }

        public IReadOnlyList<EmailAddress> BccRecipients { get; set; }

        public string Body { get; set; }

        public IReadOnlyList<EmailAddress> CcRecipients { get; set; }

        public Guid Id { get; set; }

        public IReadOnlyList<EmailAddress> Recipients { get; set; }

        public EmailAddress Sender { get; set; }

        public string Subject { get; set; }
    }
}
