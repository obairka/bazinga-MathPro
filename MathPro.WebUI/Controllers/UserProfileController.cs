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
         
        // GET: /UserProfile/id
        public async Task<ActionResult> Index(string username)
        {
            if (User.Identity.GetUserName().Equals(username))
            {
                // it's me
                return RedirectToAction("Me");
            }
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (null == user)
            {
                // TODO:
                return RedirectToAction("Index", "Home");
            }
            return View(new UserProfileViewModel(user));
        }

        
        // GET: /UserProfile/Me
        public async Task<ActionResult> Me()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
         
            return View(new UserProfileViewModel(user));
        }

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
                user.UserImageName = editUser.UserImageName;

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

        
        // Simple image checker :D
        private static bool IsImage(HttpPostedFileBase postedFile)
        {
            try  {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                    if(bitmap.Size.IsEmpty)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }



        [HttpPost]
        public async Task<ActionResult> FileUpload(HttpPostedFileBase file)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (null == user)
            {
                return HttpNotFound();
            }

            if (file != null)
            {
                if (!IsImage(file))
                {
                    // TODO:
                    return RedirectToAction("Edit", "UserProfile");
                }

                if (user.HasImage)
                {
                    // Delete old image
                    string fullPath = Request.MapPath("~/Images/UserImages/"+ user.UserImageName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                string pic = System.IO.Path.GetFileName(file.FileName);
                user.UserImageName = pic;

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                }


                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/UserImages"), pic);
                // file is uploaded
                file.SaveAs(path);

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Edit", "UserProfile");
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