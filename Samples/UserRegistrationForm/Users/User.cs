using System;
using TravisRFrench.UI.MVVM.Samples.Common;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users
{
    public class User : IUser
    {
        public static IUser Create(
            Guid id,
            string firstName,
            string lastName,
            string emailAddress,
            string username,
            string passwordHash,
            UserPreferences preferences = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            }

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            }

            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Email address cannot be empty.", nameof(emailAddress));
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be empty.", nameof(username));
            }

            if (string.IsNullOrEmpty(passwordHash))
            {
                throw new ArgumentException("Password cannot be empty.", nameof(passwordHash));
            }
            
            var user = new User(id);
            
            user.ExecuteTransaction(new NameChangeTransaction(firstName, lastName));
            user.ExecuteTransaction(new EmailAddressChangeTransaction(emailAddress));
            user.ExecuteTransaction(new UsernameChangeTransaction(username));
            user.ExecuteTransaction(new PasswordChangeTransaction(passwordHash));
            user.ExecuteTransaction(new UpdatePreferencesTransaction(preferences));

            return user;
        }

        public static IUser Create(
            string firstName,
            string lastName,
            string emailAddress,
            string username,
            string passwordHash,
            UserPreferences preferences = default)
        {
            var id = Guid.NewGuid();
            
            return Create(
                id, 
                firstName, 
                lastName, 
                emailAddress, 
                username, 
                passwordHash, 
                preferences);
        }
        
        public Guid ID { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public UserPreferences Preferences { get; private set; }

        private User(Guid id)
        {
            this.ID = id;
        }
        
        public readonly struct NameChangeTransaction : ITransaction<User>
        {
            private readonly string firstName;
            private readonly string lastName;
            
            public NameChangeTransaction(string firstName, string lastName)
            {
                this.firstName = firstName;
                this.lastName = lastName;
            }
            
            public void Execute(User user)
            {
                user.FirstName = this.firstName;
                user.LastName = this.lastName;
            }
        }
        
        public readonly struct EmailAddressChangeTransaction : ITransaction<User>
        {
            private readonly string emailAddress;
            
            public EmailAddressChangeTransaction(string emailAddress)
            {
                this.emailAddress = emailAddress;
            }
            
            public void Execute(User user)
            {
                user.EmailAddress = this.emailAddress;
            }
        }

        public readonly struct UsernameChangeTransaction : ITransaction<User>
        {
            private readonly string userName;
            
            public UsernameChangeTransaction(string userName)
            {
                this.userName = userName;
            }
            
            public void Execute(User user)
            {
                user.UserName = this.userName;
            }
        }
        
        public readonly struct PasswordChangeTransaction : ITransaction<User>
        {
            private readonly string passwordHash;
            
            public PasswordChangeTransaction(string rawPassword)
            {
                this.passwordHash = PasswordUtilities.HashPasswordText(rawPassword);
            }

            public void Execute(User transactee)
            {
                transactee.PasswordHash = this.passwordHash;
            }
        }
        
        public readonly struct UpdatePreferencesTransaction : ITransaction<User>
        {
            private readonly UserPreferences preferences;
            
            public UpdatePreferencesTransaction(UserPreferences preferences)
            {
                this.preferences = preferences;
            }

            public void Execute(User transactee)
            {
                transactee.Preferences = this.preferences;
            }
        }

        public void ExecuteTransaction(ITransaction<User> transaction)
        {
            transaction.Execute(this);
        }
    }
}
