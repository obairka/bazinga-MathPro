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

        public int MessageId { get; set; }
        public string OtherUser { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }

        public string Created { get; set; }

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