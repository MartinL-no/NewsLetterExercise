using NewsLetterExercise.Core.DomainModel;
using NewsLetterExercise.Core.DomainServices;

namespace NewsLetterExercise.Core.ApplicationServices
{
    public class SubscriptionService
    {
        private ISubscriptionModelRespository _repository;
        private readonly IEmailSender _emailSender;

        public SubscriptionService(ISubscriptionModelRespository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        public async Task<bool> CreateSubscription(string name, string emailAddress)
        {
            var subscription = new SubscriptionModel(Guid.NewGuid(), name, emailAddress);
            var email = subscription.GetSubscriptionEmail();
            var isCreated = await _repository.Create(subscription);
            var isEmailSent = await _emailSender.SendEmail(email);

            return isCreated && isEmailSent;
        }

        public async Task<bool> ConfirmSubscription(string emailAddress, Guid confirmationCode)
        {
            var subscription = await _repository.Read(emailAddress);
            var isConfirmed = subscription.ConfirmSubscription(confirmationCode);
            var isUpdated = await _repository.Update(subscription);

            return isConfirmed && isUpdated;
        }
    }
}
