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
using MathPro.WebUI.DbContexts;
using System.Data;
using MathPro.WebUI.Models;

namespace MathPro.WebUI.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {

        public int PagesSize = 4;

        public MessageController()
        {
           
        }

        public MessageController(ApplicationUserManager userManager)
        {   
            UserManager = userManager;            
        }

        private ApplicationDb db = new ApplicationDb();
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
        public async Task<ActionResult> Index(int page=1)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //TODO:
            if (user == null)
            {
                return HttpNotFound();
            }
            // select last message with every user sorted by created date

            MessageListViewModel model = new MessageListViewModel
            {
                Messages = user.MyMessages.Select( m => new MessageViewModel(m, m.Sender, m.Recipient))
                    .OrderByDescending(mv => mv.Created)
                    .Skip( (page-1) * PagesSize)
                    .Take(PagesSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PagesSize,
                    TotalPages = user.MyMessages.Count()
                }
            };

            return View(model);
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
                    message.CreatedOn = DateTime.Now.ToUniversalTime();
                    message.IsRead = false;

                    db.Messages.Add(message);
                    db.SaveChanges();
                    

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

        public JSonResult AutoComplete(string piece)
        {
            string starter = piece.Trim();
            var userNames = UserManager.Users.ToList()
                .Where(u => u.UserName.StartsWith(starter))
                

            return null;
        }
        
    }
}