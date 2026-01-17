using TMPro;
using TravisRFrench.UI.MVVM.Core;
using TravisRFrench.UI.MVVM.DataBinding.BindingTypes;
using TravisRFrench.UI.MVVM.DataBinding.Builders;
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
        private Button submitButton;
        
        protected override void ConfigureManualBindings(IBindingRegistry registry)
        {
            BindingFactory.CreateOneWay()
                .From(this.ViewModel, vm => vm.Title)
                .To((value) => this.titleLabel.text = value)
                .Register(registry);

            BindingFactory.CreateTwoWay()
                .From(this.ViewModel, model => model.FirstName)
                .To(() => this.firstNameInputField.text,
                    (value) => this.firstNameInputField.text = value,
                    this.firstNameInputField.onValueChanged)
                .Register(registry);
            
            BindingFactory.CreateTwoWay()
                .From(this.ViewModel, model => model.LastName)
                .To(() => this.lastNameInputField.text,
                    (value) => this.lastNameInputField.text = value,
                    this.lastNameInputField.onValueChanged)
                .Register(registry);
            
            BindingFactory.CreateTwoWay()
                .From(this.ViewModel, model => model.EmailAddress)
                .To(() => this.emailAddressInputField.text,
                    (value) => this.emailAddressInputField.text = value,
                    this.emailAddressInputField.onValueChanged)
                .Register(registry);
            
            BindingFactory.CreateTwoWay()
                .From(this.ViewModel, model => model.UserName)
                .To(() => this.userNameInputField.text,
                    (value) => this.userNameInputField.text = value,
                    this.userNameInputField.onValueChanged)
                .Register(registry);
            
            BindingFactory.CreateTwoWay()
                .From(this.ViewModel, model => model.Password)
                .To(() => this.passwordInputField.text,
                    (value) => this.passwordInputField.text = value,
                    this.passwordInputField.onValueChanged)
                .Register(registry);
            
            BindingFactory.CreateTwoWay()
                .From(this.ViewModel, model => model.HasAcceptedMarketingEmails)
                .To(() => this.acceptMarketingEmailsToggle.isOn,
                    (value) => this.acceptMarketingEmailsToggle.isOn = value,
                    this.acceptMarketingEmailsToggle.onValueChanged)
                .Register(registry);

            BindingFactory.CreateCommand()
                .From(this.submitButton.onClick)
                .To(this.ViewModel.Submit)
                .Register(registry);
        }
    }
}
