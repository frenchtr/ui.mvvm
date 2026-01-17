using TravisRFrench.UI.MVVM.Core;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Form
{
    public interface IUserRegistrationFormViewModel : IViewModel
    {
        string Title { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        bool HasAcceptedMarketingEmails { get; set; }

        void Submit();
    }
}
