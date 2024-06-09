using System;

namespace ChatApp.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string Room { get; set; }
    }
}
