using System;
using TravisRFrench.UI.MVVM.Samples.Common;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users;
using UnityEngine;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Services
{
    public class UserDatabaseService : IUserDatabaseService
    {
        public IUser LoadUser(Guid id)
        {
            // Simulate loading a user from the database
            Debug.Log($"Loading user with id '{id}' from database.");
            var user = User.Create(id, "John", "Smith", "john.smith@company.com", "john.smith", PasswordUtilities.HashPasswordText("N0TaReaLP@$$w0rd!"));

            return user;
        }
        public void SaveUser(IUser user)
        {
            Debug.Log($"Saving user with id '{user.ID}' to database.");
        }
    }
}
