namespace TravisRFrench.UI.MVVM.Samples.UserRegistrationForm.Users
{
    public readonly struct UserPreferences
    {
        public bool IsAllowedToReceiveMarketingEmails { get; }
        
        public UserPreferences(bool isAllowedToReceiveMarketingEmails = false)
        {
            this.IsAllowedToReceiveMarketingEmails = isAllowedToReceiveMarketingEmails;
        }
    }
}
