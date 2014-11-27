using MathPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MathPro.WebUI.Models
{
    public class MessageViewModel
    {
        public MessageViewModel() { }
        public MessageViewModel(Message message, string me)
        {
            MessageId = message.MessageId;
            SenderImageName = message.Sender.UserImageName;
            SenderUserName = message.Sender.UserName;
            RecipientUserName = message.Recipient.UserName;
            RecipientImageName = message.Recipient.UserImageName;

            CreatedOn = message.CreatedOn;
            IsRead = message.IsRead;
            Subject = message.Subject;
            Body = message.Body;
            WatcherUserName = me;
        }

        public string WatcherUserName { get; set; }
        public int MessageId { get; set; }
        
        public string SenderUserName { get; set; }
        public string SenderImageName { get; set; }

        public string RecipientUserName { get; set; }
        public string RecipientImageName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsRead { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

    public class MessageSendModel
    {
        [Required]
        public string RecipientUserName { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }

    }


    public class MessageListViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}