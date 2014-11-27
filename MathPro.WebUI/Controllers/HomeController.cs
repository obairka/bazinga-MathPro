using System.Data.Entity.Migrations;
using MathPro.Domain.Entities;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using MathPro.WebUI.Models;
using MathPro.WebUI.DbContexts;

namespace MathPro.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDb db = new ApplicationDb();
        public HomeController()
        {

        }

        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        private const int RatingPagesSize = 4;
        //
        // Ratings
        // GET:
        public async Task<ActionResult> Ratings(string sortOrder,int page = 1)
        {
            ViewBag.RatingSortParam = String.IsNullOrEmpty(sortOrder) ? "rating_desc" : "" ;

            var allUsers = await UserManager.Users.ToListAsync();
            var users = allUsers.Where(u => !UserManager.IsInRole(u.Id, "Admin"))
                .Select(s => new UserProfileBriefViewModel(s));          

            switch (sortOrder)
            {
                case "rating_desc":
                    users = users.OrderByDescending(u => u.Rating);
                    break;
                default:
                    users = users.OrderBy(u => u.Rating);
                    break;
            }


            UserRatingViewModel userRatingViewModel = new UserRatingViewModel
            {
                Users = users
                    .Skip( (page-1) * RatingPagesSize)
                    .Take(RatingPagesSize)
                    .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = RatingPagesSize,
                    TotalItems = users.Count()
                }
            };

            return View(userRatingViewModel);
        }

        [HttpGet]
        public ActionResult Assignments()
        {
            MathAssignmentViewModel maSm = new MathAssignmentViewModel();
            maSm.subsections = new SubsectionViewModel();
            maSm.subsections.AvailableSubsection = new List<Subsection>(db.Subsections);
            maSm.subsections.SelectedSubsection = new List<Subsection>();
            maSm.sections = db.Sections;
            maSm.complexities = db.Complexities;
            maSm.mathAssignments = db.MathAssignments.ToList();
            return View(maSm);
        }

        [HttpPost]
        public ActionResult Assignments(MathAssignmentViewModel maSm)
        {
            MathAssignmentViewModel maSmToView = new MathAssignmentViewModel();
            maSmToView.subsections = new SubsectionViewModel();
            List<int> subsectionFilter = new List<int>();
            //if some subsections was chosen to find, else we don't need to look at this parameter
            if (maSm.subsections != null)
            {
                var availableSubsections = new List<Subsection>(db.Subsections);
                var selectedSubsections = new List<Subsection>();
                var postedSubsectionIds = new string[0];
                if (maSm.subsections.PostedSubsection == null)
                    maSm.subsections.PostedSubsection = new PostedSubsections();

                // if a view model array of posted seubsections ids exists
                // and is not empty,save selected ids
                if (maSm.subsections.PostedSubsection.SubsectionIds != null &&
                    maSm.subsections.PostedSubsection.SubsectionIds.Any())
                {
                    postedSubsectionIds = maSm.subsections.PostedSubsection.SubsectionIds;
                }

                // if there are any selected ids saved, create a list of seubsections
                if (maSm.subsections.PostedSubsection.SubsectionIds.Any())
                {
                    selectedSubsections = availableSubsections
                        .Where(x => postedSubsectionIds.Any(s => x.SubsectionId.ToString().Equals(s)))
                        .ToList();
                }

                maSm.subsections.AvailableSubsection = new List<Subsection>(db.Subsections);

                maSm.subsections.SelectedSubsection = selectedSubsections;
                maSm.mathAssignment.Subsections = selectedSubsections;

                subsectionFilter =
                    db.MathAssignments.ToList()
                        .Where(a => selectedSubsections.All(subs => a.Subsections.Contains(subs)))
                        .Select(c => c.MathAssignmentId)
                        .ToList();
                maSmToView.subsections.SelectedSubsection = selectedSubsections;
            }

            maSm.mathAssignment.Complexity = db.Complexities.Find(maSm.mathAssignment.ComplexityId);
            maSm.mathAssignment.Section = db.Sections.Find(maSm.mathAssignment.SectionId);

            var complexityFilter =
                db.MathAssignments.Where(c => c.ComplexityId == maSm.mathAssignment.ComplexityId)
                    .Select(c => c.MathAssignmentId)
                    .ToList();
            var sectionFilter =
                db.MathAssignments.Where(c => c.SectionId == maSm.mathAssignment.SectionId)
                    .Select(c => c.MathAssignmentId)
                    .ToList();


            //List<int> filter = new List<int>();
            //find distinct MathAssignmentIds to view
            List<int> filter = (
                maSm.mathAssignment.Complexity != null || complexityFilter.Count != 0
                    ? complexityFilter.Intersect(maSm.mathAssignment.Subsections != null || subsectionFilter.Count != 0
                        ? subsectionFilter
                        : complexityFilter)
                        .Intersect(maSm.mathAssignment.Section != null || sectionFilter.Count != 0
                            ? sectionFilter
                            : complexityFilter).ToList()
                    : maSm.mathAssignment.Subsections != null || subsectionFilter.Count != 0
                        ? subsectionFilter.Intersect(maSm.mathAssignment.Section != null || sectionFilter.Count != 0
                            ? sectionFilter
                            : subsectionFilter).ToList()
                        : maSm.mathAssignment.Section != null || sectionFilter.Count != 0
                            ? sectionFilter
                            : new List<int>());

            //if there is nothing was chosen to filter then Assignment list should be full
            if (maSm.mathAssignment.Complexity == null && maSm.mathAssignment.Subsections == null &&
                maSm.mathAssignment.Section == null)
            {
                filter = db.MathAssignments.Select(c => c.MathAssignmentId).ToList();
            }

            var mathAssignmentToView =
                db.MathAssignments.Where(c => filter.Contains(c.MathAssignmentId)).Select(c => c);
            maSmToView.mathAssignments = mathAssignmentToView;
            maSmToView.subsections.AvailableSubsection = new List<Subsection>(db.Subsections);
            maSmToView.sections = db.Sections;
            maSmToView.complexities = db.Complexities;
            return View(maSmToView);
        }

        [HttpGet]
        public async Task<ActionResult> AssignmentView(int MathAssignmentId)
        {
            MathAssignmentViewModel maSmToView = new MathAssignmentViewModel();
            maSmToView.mathAssignment = db.MathAssignments.Find(MathAssignmentId);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //if user is not registered then redirect to register page
            if (user == null)
                return RedirectToAction("Register", "Account");
            var attemptId = db.UserAttempts.Where(c => c.ApplicationUser.Id == user.Id && c.MathAssignmentId == MathAssignmentId).Select(c => c.UserAttemptId).ToList();
            //if uses has already solved this task
            if (attemptId.Count() != 0)
            {
                maSmToView.userAttempt = db.UserAttempts.Find(attemptId.First());
            }
            else
            {
                maSmToView.userAttempt = new UserAttempt();
                maSmToView.userAttempt.ApplicationUser = user;
                maSmToView.userAttempt.MathAssignmentId = MathAssignmentId;
                maSmToView.userAttempt.MathAssignment = db.MathAssignments.Find(MathAssignmentId);
                maSmToView.userAttempt.AttemptDateTime = DateTime.Now;
                

                var comIdList = db.TaskComments.Where(c => c.MathAssignmentId == MathAssignmentId).Select(c => c.ApplicationUserId).ToList();

                foreach (var userId in comIdList)
                {
                    foreach (var comment in maSmToView.userAttempt.MathAssignment.TaskComments)
                    {
                        if(comment.ApplicationUserId == userId)
                            comment.ApplicationUser = await UserManager.FindByIdAsync(userId);
                    }
                }

            }
            return View(maSmToView);
        }

        [HttpPost]
        public ActionResult AssignmentView(MathAssignmentViewModel maSm)
        {
            UserAttempt userAttempt = new UserAttempt();
            userAttempt.AttemptDateTime = DateTime.Now;
            userAttempt.MathAssignmentId = maSm.userAttempt.MathAssignmentId;
            userAttempt.MathAssignment = db.MathAssignments.Find(userAttempt.MathAssignmentId);
            userAttempt.AssignmentAnswer = maSm.userAttempt.AssignmentAnswer;
            userAttempt.ApplicationUser = db.Users.Find(maSm.userAttempt.ApplicationUser.Id);
            // TODO:
            userAttempt.AttemptResultSuccess = false;
            
            db.UserAttempts.Add(userAttempt);
            db.SaveChanges();
            return RedirectToAction("Assignments");
        }

        [HttpPost]
        public ActionResult CommentAdd(MathAssignmentViewModel maSm)
        {
            TaskComment taskComment = new TaskComment();
            taskComment.Details = maSm.taskComment.Details;
            taskComment.MathAssignmentId = maSm.userAttempt.MathAssignmentId;
            taskComment.MathAssignment = db.MathAssignments.Find(taskComment.MathAssignmentId);
            taskComment.PostedTime = DateTime.Now;
            taskComment.ApplicationUser = db.Users.Find(maSm.userAttempt.ApplicationUser.Id);
            taskComment.ApplicationUserId = db.Users.Find(maSm.userAttempt.ApplicationUser.Id).Id;
            maSm.taskComment = taskComment;
            db.TaskComments.Add(taskComment);
            db.SaveChanges();
            return RedirectToAction("AssignmentView", new { MathAssignmentId = maSm.userAttempt.MathAssignmentId });
        }
    }
}