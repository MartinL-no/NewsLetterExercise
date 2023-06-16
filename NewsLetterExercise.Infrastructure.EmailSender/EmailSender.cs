using System.Net;
using NewsLetterExercise.Core.DomainModel;
using NewsLetterExercise.Core.DomainServices;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;

namespace NewsLetterExercise.Infrastructure.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient _client;

        public EmailSender()
        {
             var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            _client = new SendGridClient(apiKey);
            //"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"
        }

        public async Task<bool> SendEmail(EmailModel email)
        {
            var from = new EmailAddress("martin@getacademy.no", "Confirmation Team");
            var to = new EmailAddress(email.To, email.Name);
            var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await _client.SendEmailAsync(msg).ConfigureAwait(false);

            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}