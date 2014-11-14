using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace MathPro.WebUI.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel : UserProfileViewModel
    {
        
        public string Id { get; set; }
    
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}