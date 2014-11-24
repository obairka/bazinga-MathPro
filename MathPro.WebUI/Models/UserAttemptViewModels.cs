using MathPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathPro.WebUI.Models
{
    public class UserAttemptsListViewModel
    {
        public IEnumerable<UserAttemptItem> UserAttempts { get; set; }        
        public PagingInfo PagingInfo { get; set; }
    }
    public class UserAttemptItem
    {

        public int UserAttemptId { get; set; }
        public int MathAssignmentId { get; set; }

        public DateTime AttemptDateTime { get; set; }
        
        // Value obtained after comparing AssignmentAnswer and MathAssignment.Answer
        public bool AttemptResultSuccess { get; set; }

        public int Points { get; set; }
    }

}