using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Services;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Form
{
    public class UserRegistrationFormViewModel : ViewModel, IUserRegistrationFormViewModel
    {
        private readonly IUserRegistrationService userRegistrationService;
        private string userName;
        private string passwordHash;
        private string emailAddress;
        private string firstName;
        private string lastName;
        private bool isAllowedToReceiveMarketingEmails;

        public string UserName
        {
            get => this.userName;
            set => this.SetField(ref this.userName, value);
        }

        public string PasswordHash
        {
            get => this.passwordHash;
            set => this.SetField(ref this.passwordHash, value);
        }

        public string EmailAddress
        {
            get => this.emailAddress;
            set => this.SetField(ref this.emailAddress, value);
        }

        public string FirstName
        {
            get => this.firstName;
            set => this.SetField(ref this.firstName, value);
        }

        public string LastName
        {
            get => this.lastName;
            set => this.SetField(ref this.lastName, value);
        }

        public bool IsAllowedToReceiveMarketingEmails
        {
            get => this.isAllowedToReceiveMarketingEmails;
            set => this.SetField(ref this.isAllowedToReceiveMarketingEmails, value);
        }

        public void Submit()
        {
            // Validate/sanitize input
            // TODO
            
            // Create the user
            var preferences = new UserPreferences(
                isAllowedToReceiveMarketingEmails: this.IsAllowedToReceiveMarketingEmails);
            
            var user = User.Create(
                this.FirstName, 
                this.LastName, 
                this.EmailAddress, 
                this.UserName,
                this.PasswordHash, 
                preferences
                );
            
            // Register the user
            this.userRegistrationService.RegisterUser(user);
        }
    }
}
