using Moq;
using NewsLetterExercise.Core.ApplicationServices;
using NewsLetterExercise.Core.DomainServices;
using NewsLetterExercise.Core.DomainModel;

namespace NewsLetterExercise.UnitTest
{
    public class SubscriptionServiceTest
    {
        [Test]
        public async Task Test_CreateSubscription()
        {
            var name = "test";
            var emailAddress = "test@test.com";
            var repositoryMock = new Mock<ISubscriptionModelRespository>();
            var emailMock = new Mock<IEmailSender>();
            repositoryMock.Setup(repo => repo.Create(It.IsAny<SubscriptionModel>())).Returns(Task.FromResult(true));
            emailMock.Setup(email => email.SendEmail(It.IsAny<EmailModel>())).Returns(Task.FromResult(true));
            var subscriptionService = new SubscriptionService(repositoryMock.Object, emailMock.Object);
            
            var isCreatedAndEmailSent = await subscriptionService.CreateSubscription(name, emailAddress);

            Assert.IsTrue(isCreatedAndEmailSent);
            repositoryMock.Verify(repo => repo.Create(It.IsAny<SubscriptionModel>()), Times.Once);
            emailMock.Verify(email => email.SendEmail(It.IsAny<EmailModel>()), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
            emailMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Test_ConfirmSubscription()
        {
            var name = "test";
            var email = "test@test.com";
            var subscription = new SubscriptionModel(Guid.NewGuid(), name, email);
            var mock = new Mock<ISubscriptionModelRespository>();
            mock.Setup(repo => repo.Read(It.IsAny<string>()))
                .Returns(Task.FromResult(subscription));
            mock.Setup(repo => repo.Update(It.IsAny<SubscriptionModel>()))
                .Returns(Task.FromResult(true));
            var subscriptionService = new SubscriptionService(mock.Object, It.IsAny<IEmailSender>());

            var isConfirmed = await subscriptionService.ConfirmSubscription(email, subscription.ConfirmationCode);

            Assert.IsTrue(isConfirmed);
            mock.Verify(repo => repo.Read(email), Times.Once);
            mock.Verify(repo => repo.Update(subscription), Times.Once);
            mock.VerifyNoOtherCalls();
        }
    }
}
