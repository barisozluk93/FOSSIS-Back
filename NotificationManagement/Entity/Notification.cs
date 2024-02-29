using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationManagement.Entity
{
    public class Notification
    {
        public long Id { get; set; }

        public string Message { get; set; }

        public long UserId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsReaded { get; set; } = false;
    }
}
