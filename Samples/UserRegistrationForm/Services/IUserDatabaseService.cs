using System;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Services
{
    public interface IUserDatabaseService
    {
        IUser LoadUser(Guid id);
        void SaveUser(IUser user);
    }
}
