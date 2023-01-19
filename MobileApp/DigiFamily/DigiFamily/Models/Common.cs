namespace DigiFamily.Models
{
    public enum ToastType
    {
        Info,
        Success,
        Warning,
        Error
    }

    public enum GenderType
    {
        Male,
        Female
    }

    public enum AlertType
    {
        Notify,
        Confirm
    }

    public enum DetailPageType
    {
        Add,
        Update
    }
    public class ToastContent
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ToastType ToastType { get; set; }
    }
    public class AlertContent
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string OkBtn { get; set; }
        public AlertType AlertType { get; set; }
        public bool IsConfirmed { get; set; }
        public string NoBtn { get; set; }
        public string YesBtn { get; set; }
    }
}
