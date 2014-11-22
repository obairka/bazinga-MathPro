using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MathPro.WebUI.Models;
using MathPro.Domain.Entities;
using MathPro.WebUI.DbContexts;

namespace MathPro.WebUI.Controllers
{    
    public class UserProfileController : Controller
    {
        public UserProfileController()
        {
           
        }

        public UserProfileController(ApplicationUserManager userManager)
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
         

        // GET: /UserProfile/id
        public async Task<ActionResult> Index(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(new UserProfileViewModel(user));
        }

        
        // GET: /UserProfile/Me
        [Authorize]
        public async Task<ActionResult> Me()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return View(new UserProfileViewModel(user));
        }

        // GET: /UserProfile/Edit
        [Authorize]
        public async Task<ActionResult> Edit()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //TODO:
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new UserProfileViewModel(user));
        }

        //
        // POST: /UserProfile/Edit
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserProfileViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                // TODO:
                if (user == null)
                {
                    return HttpNotFound();
                }
                
                // This field User can edit
                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.BirthDate = editUser.BirthDate;

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                }

                return RedirectToAction("Me");
            }
            else
            {
                // TODO:
                ModelState.AddModelError("", "Something failed.");
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(editUser);
            }
        }
        [Authorize]
        // Get
        public ActionResult BriefProfile()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (null == user)
            {
                return HttpNotFound();
            }
            return PartialView("_BriefProfile",new UserProfileBriefViewModel(user));            
        }
        
    }
}