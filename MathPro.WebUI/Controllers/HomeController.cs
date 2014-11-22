using MathPro.Domain.Entities;
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


        //
        // Ratings
        // GET:
        public async Task<ActionResult> Ratings()
        {
            var users = await UserManager.Users.ToListAsync();
            var result = users.Where(u => !UserManager.IsInRole(u.Id, "Admin"))
                .Select(s => new UserProfileBriefViewModel(s)).ToList();

            return View(result);
        }

        [HttpGet]
        public ActionResult Assignments()
        {
            ApplicationDb db = new ApplicationDb();
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
            ApplicationDb db = new ApplicationDb();
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

                subsectionFilter = db.MathAssignments.ToList().Where(a => selectedSubsections.All(subs => a.Subsections.Contains(subs))).Select(c => c.MathAssignmentId).ToList();
                maSmToView.subsections.SelectedSubsection = selectedSubsections;
            }

            maSm.mathAssignment.Complexity = db.Complexities.Find(maSm.mathAssignment.ComplexityId);
            maSm.mathAssignment.Section = db.Sections.Find(maSm.mathAssignment.SectionId);

            var complexityFilter = db.MathAssignments.Where(c => c.ComplexityId == maSm.mathAssignment.ComplexityId).Select(c => c.MathAssignmentId).ToList();
            var sectionFilter = db.MathAssignments.Where(c => c.SectionId == maSm.mathAssignment.SectionId).Select(c => c.MathAssignmentId).ToList();
            

            //List<int> filter = new List<int>();
            //find distinct MathAssignmentIds to view
            List<int> filter = (
                maSm.mathAssignment.Complexity != null || complexityFilter.Count != 0
                    ? complexityFilter.Intersect(maSm.mathAssignment.Subsections != null || subsectionFilter.Count != 0 ? subsectionFilter : complexityFilter)
                        .Intersect(maSm.mathAssignment.Section != null || sectionFilter.Count != 0 ? sectionFilter : complexityFilter).ToList() :
                maSm.mathAssignment.Subsections != null || subsectionFilter.Count != 0
                    ? subsectionFilter.Intersect(maSm.mathAssignment.Section != null || sectionFilter.Count != 0 ? sectionFilter : subsectionFilter).ToList() :
                maSm.mathAssignment.Section != null || sectionFilter.Count != 0
                    ? sectionFilter : new List<int>());

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
    }
}