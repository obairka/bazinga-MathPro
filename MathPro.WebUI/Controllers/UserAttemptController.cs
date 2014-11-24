using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MathPro.WebUI.Controllers
{
    [Authorize]
    public class UserAttemptController : Controller
    {
        // GET: UserAttempt
        public ActionResult Index()
        {
            return View();
        }
        
        // TODO: customize routes configurations
        // Get UserAttempt/MyAttempts
        public ActionResult MyAttempts()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MyAttempts()
        {
            return View();
        }

        // GET
        public ActionResult ShowAttempt(string attemptId)
        {
            return View();
        }




    }
}