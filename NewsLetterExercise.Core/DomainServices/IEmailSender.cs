using NewsLetterExercise.Core.DomainModel;

namespace NewsLetterExercise.Core.DomainServices
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailModel email);
    }
}
