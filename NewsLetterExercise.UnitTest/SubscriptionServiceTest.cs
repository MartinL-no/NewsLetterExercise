using Moq;
using NewsLetterExercise.Core.ApplicationServices;
using NewsLetterExercise.Core.DomainModel;

namespace NewsLetterExercise.UnitTest
{
    public class SubscriptionServiceTest
    {
        [Test]
        public async Task Test_Create()
        {
            var mock = new Mock<ISubscriptionModelRespository>();
            mock.Setup(repo => repo.Create(It.IsAny<SubscriptionModel>())).Returns(Task.FromResult(true));
            var subscriptionService = new SubscriptionService(mock.Object);
            var subscription = await subscriptionService.Create("test", "test@test.com");

            Assert.IsNotNull(subscription);
            mock.Verify(repo => repo.Create(subscription), Times.Once);
            mock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task Test_Update()
        {
            var name = "test";
            var email = "test@test.com";
            var subscriptionOne = new SubscriptionModel(Guid.NewGuid(), name, email);
            var confirmationCode = subscriptionOne.ConfirmationCode;
            var mock = new Mock<ISubscriptionModelRespository>();
            mock.Setup(repo => repo.Read(It.IsAny<string>()))
                .Returns(Task.FromResult(subscriptionOne));
            mock.Setup(repo => repo.Update(It.IsAny<SubscriptionModel>()))
                .Returns(Task.FromResult(true));
            var subscriptionService = new SubscriptionService(mock.Object);

            var subscriptionTwo = await subscriptionService.Update(email, confirmationCode);

            Assert.AreEqual(subscriptionOne, subscriptionTwo);
            Assert.IsTrue(subscriptionTwo.IsConfirmed);
            mock.Verify(repo => repo.Read(email), Times.Once);
            mock.Verify(repo => repo.Update(subscriptionOne), Times.Once);
            mock.VerifyNoOtherCalls();
        }
    }
}
