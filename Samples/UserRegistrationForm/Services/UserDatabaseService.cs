using System;
using System.Collections.Generic;
using System.Linq;
using TravisRFrench.UI.MVVM.Samples.Common;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users;
using UnityEngine;
using Exception = System.Exception;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Services
{
    public class UserDatabaseService : IUserDatabaseService
    {
        private readonly Dictionary<Guid, IUser> simulatedDatabaseUsers;

        public UserDatabaseService()
        {
            this.simulatedDatabaseUsers = new Dictionary<Guid, IUser>();
        }
        
        public IUser LoadUser(Guid id)
        {
            if (!this.simulatedDatabaseUsers.TryGetValue(id, out var user))
            {
                throw new Exception($"A user with id '{id}' could not be found in the database.");
            }
            
            return user;
        }
        
        public void SaveUser(IUser user)
        {
            if (user.ID == Guid.Empty)
            {
                throw new Exception($"The user being saved had an uninitialized ID.");
            }
            
            this.simulatedDatabaseUsers.Add(user.ID, user);
        }
    }
}
