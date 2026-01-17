using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users;
using UnityEngine;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserDatabaseService userDatabaseService;
        
        public UserRegistrationService(IUserDatabaseService userDatabaseService)
        {
            this.userDatabaseService = userDatabaseService;
        }
        
        public void RegisterUser(IUser user)
        {
            this.userDatabaseService.SaveUser(user);
            Debug.Log($"User with username '{user.UserName}' has been registered.");
        }
    }
}
