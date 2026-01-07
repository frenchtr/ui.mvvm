using TravisRFrench.Dependencies.Containers;
using TravisRFrench.Dependencies.Installers;
using TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Services;

namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Form
{
    public class UserRegistrationFormInstaller : MonoInstaller
    {
        public override void InstallBindings(IContainer container)
        {
            container.Bind<IUserDatabaseService>()
                .To<UserDatabaseService>()
                .FromNew()
                .AsSingleton();
            
            container.Bind<IUserRegistrationService>()
                .To<UserRegistrationService>()
                .FromNew()
                .AsSingleton();

            container.Bind<IUserRegistrationFormViewModel>()
                .To<UserRegistrationFormViewModel>()
                .FromNew()
                .AsSingleton();
        }
    }
}
