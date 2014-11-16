using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using MathPro.WebUI.Models;

namespace MathPro.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
           
        }

        public HomeController(ApplicationUserManager userManager)
        {   
            UserManager = userManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        //
        // Rating
        // GET:
        public async Task<ActionResult> Ratings()
        {
            IList<UserProfileBriefViewModel> list = new List<UserProfileBriefViewModel>();
            await UserManager.Users.ForEachAsync(u => list.Add(new UserProfileBriefViewModel(u)));                 
            
            return View(list);
        }
    }
}