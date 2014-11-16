using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using MathPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MathPro.Domain.Concrete;
using System.Data;

namespace MathPro.WebUI.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        public MessageController()
        {
           
        }

        public MessageController(ApplicationUserManager userManager)
        {   
            UserManager = userManager;
            
        }

        private EFDbContext db = new EFDbContext();
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
         
        //
        // GET: 
        public ActionResult Index()
        {
            return View();
        }
        // GET: 
        public async Task<ActionResult> Send()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //TODO:
            if (user == null)
            {
                return HttpNotFound();
            }
            Message message = new Message
            {
                Sender = user
            };
            return View(message);
        }

        //
        // POST: /
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Send(Message message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // check recipient id
                    var user = await UserManager.FindByIdAsync(message.RecipientId);

                    // TODO:
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    message.Created = DateTime.Now.ToUniversalTime();

                    db.Messages.Add(message);
                    db.SaveChanges();

                    // Send notify


                    // TODO:
                    return RedirectToAction("Send");

                }
            }
            catch (DataException /* dex */)
            {
                // TODO: Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(message);
        }

        public ActionResult RedirectAction { get; set; }
    }
}