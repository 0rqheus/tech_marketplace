namespace Marketplace.Models
{
    public enum NotificationType
    {
        PriceTrack = 1,
        Purchase,
        Sale,
        Report
    }

    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public int UserId { get; set; }

        public Notification () 
        { }

        public Notification(string message, NotificationType type, int userId)
        {
            Message = message;
            Type = type;
            UserId = userId;
        }
    }
}
