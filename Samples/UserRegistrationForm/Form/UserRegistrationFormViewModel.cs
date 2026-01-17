using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.Samples.Common;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Services;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Form
{
    public class UserRegistrationFormViewModel : ViewModel, IUserRegistrationFormViewModel
    {
        private readonly IUserRegistrationService userRegistrationService;
        private string title;
        private string userName;
        private string password;
        private string emailAddress;
        private string firstName;
        private string lastName;
        private bool hasAcceptedMarketingEmails;

        public UserRegistrationFormViewModel(IUserRegistrationService userRegistrationService)
        {
            this.userRegistrationService = userRegistrationService;
        }

        public string Title
        {
            get => this.title;
            set => this.SetField(ref this.title, value);
        }

        public string UserName
        {
            get => this.userName;
            set => this.SetField(ref this.userName, value);
        }

        public string Password
        {
            // Note: In a production setting we would never store the plain-text password in our view model,
            // but store the hash instead. In this example we defer hashing until submission time so that 
            // it doesn't bog down our as-you-type updates.
            get => this.password;
            set => this.SetField(ref this.password, value);
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

        public bool HasAcceptedMarketingEmails
        {
            get => this.hasAcceptedMarketingEmails;
            set => this.SetField(ref this.hasAcceptedMarketingEmails, value);
        }

        public void Submit()
        {
            // Validate/sanitize input
            // TODO
            
            // Create the user
            var preferences = new UserPreferences(
                isAllowedToReceiveMarketingEmails: this.HasAcceptedMarketingEmails);
            
            var user = User.Create(
                this.FirstName, 
                this.LastName, 
                this.EmailAddress, 
                this.UserName,
                PasswordUtilities.HashPasswordText(this.Password), 
                preferences
                );
            
            // Register the user
            this.userRegistrationService.RegisterUser(user);
        }
    }
}
