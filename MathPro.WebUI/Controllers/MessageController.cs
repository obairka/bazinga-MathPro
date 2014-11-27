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
        // todo:
        private const int PagesSize = 3;

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

        public MessageController()
        {
           
        }

        public MessageController(ApplicationUserManager userManager)
        {   
            UserManager = userManager;            
        }


        private MessageListViewModel formMessageList(string direction, int page)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            //TODO:
            if (user == null)
            {
                return null;
            }

            ICollection<Message> messageList = null;
            int totalItems = 0;
            switch (direction)
            {
                case "in":
                    messageList = user.MessagesIReceive;
                    totalItems = user.MessagesIReceive.Count();
                    break;
                case "out":
                default:
                    messageList = user.MessagesISend;
                    totalItems = user.MessagesISend.Count();
                    break;
            }
            MessageListViewModel model = new MessageListViewModel
            {
                Messages = messageList.Select(m => new MessageViewModel(m, user.UserName))
                    .OrderByDescending(mv => mv.CreatedOn)
                    .Skip((page - 1) * PagesSize)
                    .Take(PagesSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PagesSize,
                    TotalItems = totalItems,
                }
            };
            return model;
        }

        public ActionResult IncomingBox(int page = 1)
        {
            MessageListViewModel model = formMessageList("in", page);
            if (null == model)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult OutBox(int page = 1)
        {
            MessageListViewModel model = formMessageList("out", page);
            if (null == model)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult Reply(string username)
        {
            var user = UserManager.FindByName(username);
            if (null == user)
            {
                // TODO:
                return HttpNotFound();
            }

            MessageSendModel msg = new MessageSendModel
            {
                RecipientUserName = username,
            };
            return View("Send",msg);
        }

        public async Task<ActionResult> Read(int? messageId)
        {
            if (null == messageId)
            {
                return HttpNotFound();
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //TODO:
            if (user == null)
            {
                return HttpNotFound();
            }
            var message = db.Messages.Find(messageId);

            if (null == message)
            {
                return HttpNotFound();
            }

            if (! (message.SenderId.Equals(user.Id) || message.RecipientId.Equals(user.Id)) )
            {
                // TODO: Access denied
                return HttpNotFound();
            }


            message.IsRead = true;
            // Save that message was read
            db.Messages.Attach(message);
            var entry = db.Entry(message);
            entry.Property(e => e.IsRead).IsModified = true;
            // other changed properties
            db.SaveChanges();
            return View(new MessageViewModel(message, user.UserName));
        }
                 
        // GET: 
        public ActionResult Send()
        {
            MessageSendModel msg = new MessageSendModel
            {
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
                    return RedirectToAction("IncomingBox");

                }
            }
            catch (DataException /* dex */)
            {
                // TODO: Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(sendMessage);
        }


    }
}