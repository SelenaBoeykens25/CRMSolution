namespace CRMProject.Client.Services
{
    public class AuthenticationNotificationService
    {
        public event Action? OnAuthenticationChanged;

        public void NotifyAuthenticationChanged()
        {
            OnAuthenticationChanged?.Invoke();
        }
    }
}
