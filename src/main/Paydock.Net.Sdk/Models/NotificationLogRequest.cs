using System;

namespace Paydock.Net.Sdk.Models
{
    public class NotificationLogRequest
    {
        public string _id { get; set; }
        public bool? success { get; set; }
        public string eventTrigger { get; set; }
        public string type { get; set; }
        public DateTime? created_at_from { get; set; }
        public DateTime? created_at_to { get; set; }
        public string parent_id { get; set; }
        public string destination { get; set; }
    }
}
