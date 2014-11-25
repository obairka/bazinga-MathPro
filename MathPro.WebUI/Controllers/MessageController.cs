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
        private const int PagesSize = 4;

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
                Messages = user.MyMessages.Select( m => 
                        new MessageViewModel
                        {
                            MessageId = m.MessageId,
                            Sender = m.Sender.UserName,
                            Recipient = m.Recipient.UserName,
                            OtherUser = !m.Sender.Id.Equals(user.Id) ? m.Sender.UserName : m.Recipient.UserName,
                            IsRead = false,
                            Created = m.CreatedOn.ToString(),
                            Body = m.Body,
                            Subject = m.Subject,                 
                        })
                    .OrderByDescending(mv => mv.Created)
                    .Skip( (page-1) * PagesSize)
                    .Take(PagesSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PagesSize,
                    TotalItems = user.MyMessages.Count()
                }
            };

            return View(model);
        }

        public ActionResult Reply(string username)
        {
            MessageSendModel msg = new MessageSendModel
            {
                ShowPrevMessage = false,
                PrevMessage = null,
                RecipientUserName = username,
            };
            return View("Send",msg);
        }


        public async Task<ActionResult> Read(int messageId)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //TODO:
            if (user == null)
            {
                return HttpNotFound();
            }
            var m = db.Messages.Find(messageId);

            if (null == m)
            {
                return HttpNotFound();
            }

            if (! (m.SenderId.Equals(user.Id) || m.RecipientId.Equals(user.Id)) )
            {
                // TODO: Access denied
                return HttpNotFound();
            }

            m.IsRead = true;
            return View(new MessageViewModel
            {
                MessageId = m.MessageId,
                Sender = m.Sender.UserName,
                Recipient = m.Recipient.UserName,
                OtherUser = !m.Sender.Id.Equals(user.Id) ? m.Sender.UserName : m.Recipient.UserName,
                IsRead = false,
                Created = m.CreatedOn.ToString(),
                Body = m.Body,
                Subject = m.Subject,      
            });
        }
        

         
        // GET: 
        public ActionResult Send()
        {
            MessageSendModel msg = new MessageSendModel
            {
                ShowPrevMessage = false,
                PrevMessage = null,
            };
            return View(msg);            
        }        

        //
        // POST: /
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Send(MessageSendModel sendMessage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // check recipient id
                    var user = await UserManager.FindByNameAsync(sendMessage.RecipientUserName);
                    var me = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    // TODO:
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    Message message = new Message
                    {
                        RecipientId = user.Id,
                        //Recipient = user,
                        SenderId = me.Id,
                        //Sender = me,
                        Subject = sendMessage.Subject,
                        Body = sendMessage.Body,
                        CreatedOn = DateTime.Now.ToUniversalTime(),
                        IsRead = false,
                    };

                    db.Messages.Add(message);
                    db.SaveChanges();
                    

                    // TODO:
                    return RedirectToAction("Index");

                }
            }
            catch (DataException /* dex */)
            {
                // TODO: Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(sendMessage);
        }

        [HttpPost]
        public JsonResult AutoCompleteUserNames(string term)
        {
            string starter = term.Trim();
            var userNames = UserManager.Users.ToList()
                .Select(u => u.UserName)
                .Where(s => s.Contains(starter))
                .OrderBy(n => n)
                .Take(4)
                .ToList();

            return Json(userNames);
        }
        
    }
}