using NewsLetterExercise.Core.DomainModel;

namespace NewsLetterExercise.UnitTest
{
    public class SubscriptionModelTests
    {
        [Test]
        public void Test_ValidName()
        {
            var name = "Mr Test";
            var email = "test@test.com";
            var subscription = new SubscriptionModel(Guid.NewGuid(), name, email);
            

            Assert.AreEqual(name, subscription.Name);
        }

        [Test]
        public void Test_InvalidName()
        {
            var name = "";
            var email = "test@test.com";

            var exception = Assert.Throws<ArgumentException>(() => new SubscriptionModel(Guid.NewGuid(), name, email));
            Assert.That(exception.Message, Is.EqualTo("Name cannot be null or empty (Parameter 'name')"));
        }

        [Test]
        public void Test_ValidEmail()
        {
            var name = "Mr Test";
            var email = "test@test.com";
            var subscription = new SubscriptionModel(Guid.NewGuid(), name, email);
            
            Assert.AreEqual(email, subscription.Email);
        }

        [Test]
        public void Test_InvalidEmail()
        {
            var name = "test";
            var email = "@example.com";

            var exception = Assert.Throws<FormatException>(() => new SubscriptionModel(Guid.NewGuid(), name, email));
            Assert.That(exception.Message, Is.EqualTo("Invalid email address"));
        }

        [Test]
        public void Test_ConfirmationCode()
        {
            var name = "Mr Test";
            var email = "test@test.com";
            var subscription = new SubscriptionModel(Guid.NewGuid(), name, email);
            var confirmationCode = subscription.ConfirmationCode;

            Assert.That(confirmationCode, Is.TypeOf<Guid>());
            Assert.IsNotNull(confirmationCode);
        }

        [Test]
        public void Test_ConfirmSubscription_ValidConfirmationCode()
        {
            var name = "Mr Test";
            var email = "test@test.com";
            var subscription = new SubscriptionModel(Guid.NewGuid(), name, email);
            
            var confirmationCode = subscription.ConfirmationCode;
            bool isConfirmed = subscription.ConfirmSubscription(confirmationCode);

            Assert.IsTrue(isConfirmed);
        }

        [Test]
        public void Test_ConfirmSubscription_InValidConfirmationCode()
        {
            var name = "Mr Test";
            var email = "test@test.com";
            var subscriptionModel = new SubscriptionModel(Guid.NewGuid(), name, email);

            var invalidConfirmationCode = Guid.NewGuid();
            bool isConfirmed = subscriptionModel.ConfirmSubscription(invalidConfirmationCode);

            Assert.IsFalse(isConfirmed);
        }
    }
}