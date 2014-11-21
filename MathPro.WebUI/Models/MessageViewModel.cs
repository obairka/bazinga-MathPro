using MathPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathPro.WebUI.Models
{
    public class MessageViewModel
    {
        public MessageViewModel(Message message, ApplicationUser other)
        {
            MessageId = message.MessageId;
            IsRead = message.IsRead;
            Subject = message.Subject;
            Body = message.Body;
            Created = message.CreatedOn;

            User = other;
        
        }
        public int MessageId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime Created { get; set; }

        // TODO: Default: IsRead = false
        public bool IsRead { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

    }
}