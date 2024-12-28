namespace VacationFrontend.Notifications
{
    public class NotificationService
    {
        public event Action<string>? OnMessageChanged;

        public void ShowMessage(string message)
        {
            OnMessageChanged?.Invoke(message);
        }
    }
}
