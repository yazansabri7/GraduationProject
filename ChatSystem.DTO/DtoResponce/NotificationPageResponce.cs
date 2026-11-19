using ChatSystem.DTO.DtoResponce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSystem.DTO.DTO
{
    public class NotificationPageResponce
    {
        public List<NotificationResponce> Items { get; set; } = new();

        public bool HasMore { get; set; }

        public NotificationCursor? NextCursor { get; set; }
    }
}
