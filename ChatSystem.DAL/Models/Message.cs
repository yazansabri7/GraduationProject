using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DAL.Models
{
    public class Message:Entity
    {
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        public bool IsWrite { get; set; }

    }
}
