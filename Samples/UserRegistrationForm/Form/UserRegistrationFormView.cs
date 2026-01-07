using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.Samples.Common;
using UnityEngine;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Form
{
    public class UserRegistrationFormView : View<IUserRegistrationFormViewModel>, IUserRegistrationFormView
    {
        [SerializeField][Flatten("value", "Email")]
        private ViewProperty<string> email;
    }
}
