﻿using System.Net.Mail;

namespace NewsLetterExercise.Core.DomainModel
{
    public class SubscriptionModel : BaseModel
    {
        public readonly string Name;
        public readonly string Email;
        public readonly Guid ConfirmationCode;
        public bool IsConfirmed { get; private set; }
        public SubscriptionModel(Guid id, string name, string email) : base(id)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }

            try
            {
                var validEmail = new MailAddress(email);
                Email = validEmail.Address;

            }
            catch (FormatException)
            {
                throw new FormatException("Invalid email address");
            }

            Name = name;
            ConfirmationCode = Guid.NewGuid();
            IsConfirmed = false;
        }

        public SubscriptionModel(Guid id, string name, string email, Guid confirmationCode, bool isConfirmed): base(id)
        {
            Name = name;
            Email = email;
            ConfirmationCode = confirmationCode;
            IsConfirmed = isConfirmed;
        }

        public EmailModel GetSubscriptionEmail()
        {
            return new EmailModel(Name, Email, ConfirmationCode);
        }

        public bool ConfirmSubscription(Guid confirmationCode)
        {
            if (ConfirmationCode == confirmationCode)
            {
                IsConfirmed = true;
                return true;
            }
            return false;
        }
    }
}


/*

    Create an application where users can subscribe to and unsubscribe 
    from newsletters. The application must contain the following:

    1.  Website where you fill in your name and email address to sign up. 
    2.  The system generates a confirmation code (Guid.NewGuid in C#) and 
        sends the user an email with a link back to a page, where the email 
        and confirmation code are baked into the url as url parameters. The
        code must also be stored in a database together with the email address.
    3.  This page checks whether the code is correct in relation to what is 
        stored in the database. If so, the subscription is set to confirmed 
        and active. 
    
    In Core there should be a SubscriptionService with two methods. One is to
    create a subscription, and the other is to confirm. Create at least one 
    unit test for each of the two methods. 

    The important thing here is to practice layering and onion architecture, so
    you don't need to create a proper database and email service. For example,
    the email service can write to a file instead of actually sending an email.
    But if you want to send e-mail properly, you can use SendGrid. It's free 
    for up to 100 emails per day:
    https://sendgrid.com/marketing/sendgrid-services-cro/#pricing-app
    Use this library:  https://github.com/sendgrid/sendgrid-csharp

    What should be in the domain model?
    - SubscriptionModel:
        - Creates a new instance when a user subscribes
        - Contains name, id (guid), email, confirmation code and boolean (isConfirmed)
        - functionality
            - create user
                - creates a user object (constructor)
                - generates confirmation code (GUID)
                - user is added to database table with isConfirmed = false
                - Send emails with link containing users email and confirmation code
            - confirm user
                - takes email and confirmation code as parameters
                - checks if user exists in database (via email address) and
                if confirmation code matches
                - if so, set isConfirmed = true
*/
