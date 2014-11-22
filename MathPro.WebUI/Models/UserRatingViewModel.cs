using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MathPro.WebUI.Models
{
    public class UserRatingViewModel
    {
        public IEnumerable<UserProfileBriefViewModel> Users{ get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}