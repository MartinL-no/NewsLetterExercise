using System.Reflection;

namespace NewsLetterExercise.Core.DomainModel
{
    public class EmailModel
    {
        public string Name { get; set; }
        public string To { get; }
        public Guid ConfirmationCode { get; }
        public string Subject { get; }
        public string Body { get; set; }

        public EmailModel(string name, string email, Guid confirmationCode)
        {
            Name = char.ToUpper(name[0]) + name.Substring(1) ;;
            To = email;
            ConfirmationCode = confirmationCode;
            Subject = $"Hi {Name}, confirm your subscription!";
            Body = $"<p>Hi {Name}, Please confirm you subscription by clicking this <a href='http://localhost:5268/api/Subscription/confirm?email={To}&confirmationCode={ConfirmationCode}'>link</a></p>";
        }

    }
}
