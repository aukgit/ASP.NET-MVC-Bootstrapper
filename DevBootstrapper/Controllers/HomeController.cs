#region using block

using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DevBootstrapper.Application;
using DevBootstrapper.Models.POCO.IdentityCustomization;

#endregion

namespace DevBootstrapper.Controllers {
    public class HomeController : BasicController {
        public HomeController()
            : base(true) {
        }

        public ActionResult Index() {
            return View();
        }

        //[OutputCache(Duration=84731)]
        public ActionResult ContactUs() {
            ViewBag.FeedbackCateoryID = new SelectList(db.FeedbackCategories.ToList(), "FeedbackCategoryID", "Category");
            ViewCommon.GetTitlePageMeta(ViewBag, "Contact Us", null, "Contact Us - " + AppVar.Name,
                "Contact Us, Feedback about " + AppVar.Name);
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(Feedback feedback) {
            ViewBag.FeedbackCateoryID = new SelectList(db.FeedbackCategories.ToList(), "FeedbackCategoryID", "Category");
            ViewCommon.GetTitlePageMeta(ViewBag, "Contact Us", null, "Contact Us - " + AppVar.Name,
                "Contact Us, Feedback about " + AppVar.Name);


            if (ModelState.IsValid) {
                db.Entry(feedback).State = EntityState.Added;
                db.SaveChanges();
                ViewCommon.SetSavedStatus(ViewBag);
                //send a email.
                AppVar.Mailer.NotifyAdmin("A feedback has been added by " + feedback.Email,
                    "Please check your feedback inbox. Feedback :<br>" + feedback.Message);

                return View(feedback);
            }

            ViewCommon.SetErrorStatus(ViewBag);
            return View(feedback);
        }
    }
}