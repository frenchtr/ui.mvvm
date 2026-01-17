using TMPro;
using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using TravisRFrench.UI.MVVM.DataBinding.Registration;
using TravisRFrench.UI.MVVM.DataBinding.SubscriptionAdapters;
using TravisRFrench.UI.MVVM.Samples.Common;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        [SerializeField]
        private Toggle acceptMarketingEmailsToggle;
        [SerializeField]
        private Button button;
        
        protected override void ConfigureManualBindings(IBindingRegistry registry)
        {
            var subscriptionFactory = new SubscriptionFactory();
            var titleSubscription = subscriptionFactory.FromProperty(
                this.ViewModel, 
                nameof(this.ViewModel.Title));
            var titleBinding = new OneWayBinding<string>(
                () => this.ViewModel.Title, 
                (value) => this.titleLabel.text = value, 
                titleSubscription);

            var firstNameViewModelSubscription = subscriptionFactory.FromProperty(
                this.ViewModel, 
                nameof(this.ViewModel.FirstName));
            var firstNameViewSubscription = subscriptionFactory.FromUnityEvent(
                this.firstNameInputField.onValueChanged);
            var firstNameBinding = new TwoWayBinding<string>(
                () => this.ViewModel.FirstName,
                (value) => this.ViewModel.FirstName = value,
                firstNameViewModelSubscription,
                () => this.firstNameInputField.text,
                (value) => this.firstNameInputField.text = value,
                firstNameViewSubscription);

            var lastNameViewModelSubscription = subscriptionFactory.FromProperty(
                this.ViewModel, 
                nameof(this.ViewModel.LastName));
            var lastNameViewSubscription = subscriptionFactory.FromUnityEvent(
                this.lastNameInputField.onValueChanged);
            var lastNameBinding = new TwoWayBinding<string>(
                () => this.ViewModel.LastName,
                (value) => this.ViewModel.LastName = value,
                lastNameViewModelSubscription,
                () => this.lastNameInputField.text,
                (value) => this.lastNameInputField.text = value,
                lastNameViewSubscription);
            
            var emailAddressViewModelSubscription = subscriptionFactory.FromProperty(
                this.ViewModel, 
                nameof(this.ViewModel.EmailAddress));
            var emailAddressViewSubscription = subscriptionFactory.FromUnityEvent(
                this.emailAddressInputField.onValueChanged);
            var emailAddressBinding = new TwoWayBinding<string>(
                () => this.ViewModel.EmailAddress,
                (value) => this.ViewModel.EmailAddress = value,
                emailAddressViewModelSubscription,
                () => this.emailAddressInputField.text,
                (value) => this.emailAddressInputField.text = value,
                emailAddressViewSubscription);

            var usernameViewModelSubscription = subscriptionFactory.FromProperty(
                this.ViewModel, 
                nameof(this.ViewModel.UserName));
            var usernameViewSubscription = subscriptionFactory.FromUnityEvent(
                this.userNameInputField.onValueChanged);
            var usernameBinding = new TwoWayBinding<string>(
                () => this.ViewModel.UserName,
                (value) => this.ViewModel.UserName = value,
                usernameViewModelSubscription,
                () => this.userNameInputField.text,
                (value) => this.userNameInputField.text = value,
                usernameViewSubscription);

            var passwordViewModelSubscription = subscriptionFactory.FromProperty(
                this.ViewModel, 
                nameof(this.ViewModel.Password));
            var passwordViewSubscription = subscriptionFactory.FromUnityEvent(
                this.passwordInputField.onValueChanged);
            var passwordBinding = new TwoWayBinding<string>(
                () => this.ViewModel.Password,
                (value) => this.ViewModel.Password = value,
                passwordViewModelSubscription,
                () => this.passwordInputField.text,
                (value) => this.passwordInputField.text = value,
                passwordViewSubscription);
            
            var canReceiveMarketingEmailsViewModelSubscription = subscriptionFactory.FromProperty(
                this.ViewModel, 
                nameof(this.ViewModel.HasAcceptedMarketingEmails));
            var canReceiveMarketingEmailsViewSubscription = subscriptionFactory.FromUnityEvent(
                this.acceptMarketingEmailsToggle.onValueChanged);
            var canReceiveMarketingEmailsBinding = new TwoWayBinding<bool>(
                () => this.ViewModel.HasAcceptedMarketingEmails,
                (value) => this.ViewModel.HasAcceptedMarketingEmails = value,
                canReceiveMarketingEmailsViewModelSubscription,
                () => this.acceptMarketingEmailsToggle.isOn,
                (value) => this.acceptMarketingEmailsToggle.isOn = value,
                canReceiveMarketingEmailsViewSubscription);
            
            var submitSubscription = subscriptionFactory.FromUnityEvent(this.button.onClick);
            var submitBinding = new CommandBinding(submitSubscription, this.ViewModel.Submit);
            
            registry.Register(titleBinding);
            registry.Register(firstNameBinding);
            registry.Register(lastNameBinding);
            registry.Register(emailAddressBinding);
            registry.Register(usernameBinding);
            registry.Register(passwordBinding);
            registry.Register(canReceiveMarketingEmailsBinding);
            registry.Register(submitBinding);
        }
    }
}
