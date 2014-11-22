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
        public MessageViewModel(Message message, ApplicationUser sender, ApplicationUser recipient, ApplicationUser other)
        {
            MessageId = message.MessageId;
            IsRead = message.IsRead;
            Subject = message.Subject;
            Body = message.Body;
            Created = message.CreatedOn;

            Sender = sender;
            Recipient = recipient;

            OtherUser = other;
        
        }
        public int MessageId { get; set; }

        public ApplicationUser OtherUser { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Recipient { get; set; }

        public DateTime Created { get; set; }

        // TODO: Default: IsRead = false
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

        public bool ShowPrevMessage { get; set; }
        public MessageViewModel PrevMessage { get; set; }
    }


    public class MessageListViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
}