namespace NewsLetterExercise.API.ViewModel
{
    public class CreateSubscriptionViewModel
    {
        public string Name { get; }
        public string Email { get; }

        public CreateSubscriptionViewModel(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
