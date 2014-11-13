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

        public UserProfileController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            
            UserManager = userManager;
            RoleManager = roleManager;
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


        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        /*
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }

        */

        //
        // GET: /Account/Details
        public async Task<ActionResult> Details()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return View(user);
        }

        //
        // GET: /Account/Edit
        public async Task<ActionResult> Edit()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //TODO:
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Rating = user.Rating,

            });
        }

        //
        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUser)
        {

            if (ModelState.IsValid)
            {
                // see http://stackoverflow.com/a/22835540/1264738
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                // TODO:
                if (user == null)
                {
                    return HttpNotFound();
                }

                //user.UserName = editUser.Email;
                //user.Email = editUser.Email;

                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.BirthDate = editUser.BirthDate;

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                }

                return RedirectToAction("Details");
            }
            else
            {
                ModelState.AddModelError("", "Something failed.");
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                //TODO:
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(new EditUserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Rating = user.Rating,

                });
            }
        }
        
    }
}