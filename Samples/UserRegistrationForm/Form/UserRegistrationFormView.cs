using TMPro;
using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using TravisRFrench.UI.MVVM.DataBinding.Registration;
using UnityEngine;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Form
{
    public class UserRegistrationFormView : View<IUserRegistrationFormViewModel>, IUserRegistrationFormView
    {
        [Header("Component References")]
        [SerializeField]
        private TextMeshProUGUI titleLabel;
        [SerializeField]
        private TMP_InputField firstNameInputField;
        [SerializeField]
        private TMP_InputField lastNameInputField;
        [SerializeField]
        private TMP_InputField emailAddressInputField;
        [SerializeField]
        private TMP_InputField userNameInputField;
        [SerializeField]
        private TMP_InputField passwordInputField;
        
        protected override void ConfigureManualBindings(IBindingRegistry registry)
        {
            var titleBinding = Binding.CreateOneWay<string>()
                .From(() => this.ViewModel.UserName)
                .ByPropertyChangedNotifier(this.ViewModel, nameof(this.ViewModel.UserName))
                .To((value) => this.titleLabel.text = value)
                .Build();
            
            var firstNameBinding = Binding.CreateTwoWay<string>()
                .From(() => this.ViewModel.FirstName, (value) => this.ViewModel.FirstName = value)
                .ByPropertyChangedNotifier(this.ViewModel, nameof(this.ViewModel.FirstName))
                .To(() => this.firstNameInputField.text, (value) => this.firstNameInputField.text = value)
                .ByUnityEvent(this.firstNameInputField.onValueChanged)
                .Build();
            
            var lastNameBinding = Binding.CreateTwoWay<string>()
                .From(() => this.ViewModel.LastName, (value) => this.ViewModel.LastName = value)
                .ByPropertyChangedNotifier(this.ViewModel, nameof(this.ViewModel.LastName))
                .To(() => this.lastNameInputField.text, (value) => this.lastNameInputField.text = value)
                .ByUnityEvent(this.lastNameInputField.onValueChanged)
                .Build();
            
            var emailAddressBinding = Binding.CreateTwoWay<string>()
                .From(() => this.ViewModel.EmailAddress, (value) => this.ViewModel.EmailAddress = value)
                .ByPropertyChangedNotifier(this.ViewModel, nameof(this.ViewModel.EmailAddress))
                .To(() => this.emailAddressInputField.text, (value) => this.emailAddressInputField.text = value)
                .ByUnityEvent(this.emailAddressInputField.onValueChanged)
                .Build();
            
            var userNameBinding = Binding.CreateTwoWay<string>()
                .From(() => this.ViewModel.UserName, (value) => this.ViewModel.UserName = value)
                .ByPropertyChangedNotifier(this.ViewModel, nameof(this.ViewModel.UserName))
                .To(() => this.userNameInputField.text, (value) => this.userNameInputField.text = value)
                .ByUnityEvent(this.userNameInputField.onValueChanged)
                .Build();
            
            var passwordBinding = Binding.CreateTwoWay<string>()
                .From(() => this.ViewModel.PasswordHash, (value) => this.ViewModel.PasswordHash = value)
                .ByPropertyChangedNotifier(this.ViewModel, nameof(this.ViewModel.PasswordHash))
                .To(() => this.passwordInputField.text, (value) => this.passwordInputField.text = value)
                .ByUnityEvent(this.passwordInputField.onValueChanged)
                .Build();
            
            registry.Register(titleBinding);
            registry.Register(firstNameBinding);
            registry.Register(lastNameBinding);
            registry.Register(emailAddressBinding);
            registry.Register(userNameBinding);
            registry.Register(passwordBinding);
        }
    }
}
