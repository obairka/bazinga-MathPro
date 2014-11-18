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
using MathPro.Domain.Concrete;

namespace MathPro.WebUI.Controllers
{
    [Authorize]
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
         
        //
        // GET: /UserProfile
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            // TODO: 

            return View(new UserProfileViewModel(user));
        }

        //
        // GET: /UserProfile/Edit
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

                return RedirectToAction("Index");
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
        
    }
}