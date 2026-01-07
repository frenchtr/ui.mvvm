using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Services;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Form
{
    public class UserRegistrationFormViewModel : ViewModel, IUserRegistrationFormViewModel
    {
        private readonly IUserRegistrationService userRegistrationService;
        
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAllowedToReceiveMarketingEmails { get; set; }
        
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
