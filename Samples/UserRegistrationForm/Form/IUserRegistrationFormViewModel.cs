using TravisRFrench.UI.MVVM.Core;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Form
{
    public interface IUserRegistrationFormViewModel : IViewModel
    {
        string UserName { get; set; }
        string PasswordHash { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool IsAllowedToReceiveMarketingEmails { get; set; }

        void Submit();
    }
}
