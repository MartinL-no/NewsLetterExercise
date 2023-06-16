namespace NewsLetterExercise.Infrastructure.DataAccess.Model
{
    public class SubscriptionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid ConfirmationCode { get; set; }
        public bool IsConfirmed { get; set; }

        public SubscriptionModel(Guid id, string name, string email, Guid confirmationCode, bool isConfirmed)
        {
            Id = id;
            Name = name;
            Email = email;
            ConfirmationCode = confirmationCode;
            IsConfirmed = isConfirmed;
        }

        public SubscriptionModel()
        {

        }
    }
}
