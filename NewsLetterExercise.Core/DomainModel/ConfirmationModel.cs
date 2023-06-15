namespace NewsLetterExercise.Core.DomainModel
{
    public class ConfirmationModel
    {
        public readonly string Name;
        public readonly string Email;
        public readonly Guid ConfirmationCode;

        public ConfirmationModel(string name, string email, Guid confirmationCode)
        {
            Name = name;
            Email = email;
            ConfirmationCode = confirmationCode;
        }
    }
}