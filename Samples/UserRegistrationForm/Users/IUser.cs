using System;
using TravisRFrench.UI.MVVM.Samples.Common;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users
{
    public interface IUser : ITransactee<User>
    {
        Guid ID { get; }
        string FirstName { get; }
        string LastName { get; }
        string EmailAddress { get; }
        string UserName { get; }
        string PasswordHash { get; }
        UserPreferences Preferences { get; }
    }
}
