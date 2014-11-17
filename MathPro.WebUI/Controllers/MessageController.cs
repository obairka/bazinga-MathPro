﻿using Microsoft.AspNet.Identity;
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
using MathPro.WebUI.Models;

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

      
        // 
        // GET: 
        public async Task<ActionResult> ListAll()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //TODO:
            if (user == null)
            {
                return HttpNotFound();
            }
            // select last message with every user sorted by created date

            var messages = user.MyMessages.Select(m => new MessageViewModel(m, m.RecipientId == user.Id ? m.Sender : m.Recipient));

            return View(messages);
        }

        public JsonResult Autocomplete(string term)
        {
            var result = new List<KeyValuePair<string, string>>();
            UserManager.Users.ToList().ForEach(u => result.Add(new KeyValuePair<string, string>( u.Id, u.UserName)));
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RedirectAction { get; set; }
    }
}