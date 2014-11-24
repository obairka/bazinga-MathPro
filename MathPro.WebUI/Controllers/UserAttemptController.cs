using MathPro.WebUI.DbContexts;
using MathPro.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using MathPro.Domain.Entities;


namespace MathPro.WebUI.Controllers
{
    [Authorize]
    public class UserAttemptController : Controller
    {
        private ApplicationDb db = new ApplicationDb();

        public UserAttemptController()
        {

        }

        public UserAttemptController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        // GET: UserAttempt
        public ActionResult Index()
        {
            return View();
        }

        const int PageSize = 10;

        // GET
        public ActionResult MyAttempts(string sortOrder, bool onlySuccessResult = false, int page = 1)
        {
            ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.OnlySuccessResult = !onlySuccessResult;
            
            
            var me = User.Identity.GetUserId();
            var attempts = db.UserAttempts /*.Include(a => a.MathAssignment)*/
                .Where(a => (a.ApplicationUserId.Equals(me) ))
                .Select(a => new UserAttemptItem{
                    UserAttemptId = a.UserAttemptId,
                    AttemptDateTime = a.AttemptDateTime,
                    AttemptResultSuccess = a.AttemptResultSuccess,
                    MathAssignmentId = a.MathAssignmentId,
                    Points = a.Points,
                });


            if (onlySuccessResult)
            {
                attempts = attempts.Where(a => a.AttemptResultSuccess == true);
            }
            
            switch (sortOrder)
            {
                case "Date":
                    attempts = attempts.OrderBy(a => a.AttemptDateTime);
                    break;

                case "":
                default:
                    attempts= attempts.OrderByDescending(a => a.AttemptDateTime);
                    break;

            }

            
            
            UserAttemptsListViewModel userAttemptsList = new UserAttemptsListViewModel
            {
                UserAttempts = attempts.Skip((page - 1) * PageSize)
                                       .Take(PageSize)
                                       
                                       .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = attempts.Count(),
                }
            };

            return View(userAttemptsList);
        }


        // GET
        public ActionResult ShowAttempt(int userAttemptId)
        {
            var attempt = db.UserAttempts.Find(userAttemptId);
            if (null == attempt)
            {
                return HttpNotFound();
            }

            return View(attempt);
        }




    }
}