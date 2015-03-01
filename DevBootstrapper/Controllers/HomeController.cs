using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DevBootstrapper.Models.POCO.IdentityCustomization;

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
            ViewBag.FeedbackCateoryID = new SelectList(Db.FeedbackCategories.ToList(), "FeedbackCategoryID", "Category");
            AppVar.GetTitlePageMeta(ViewBag, "Contact Us", null, "Contact Us - " + AppVar.Name,
                "Contact Us, Feedback about " + AppVar.Name);
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(Feedback feedback) {
            ViewBag.FeedbackCateoryID = new SelectList(Db.FeedbackCategories.ToList(), "FeedbackCategoryID", "Category");
            AppVar.GetTitlePageMeta(ViewBag, "Contact Us", null, "Contact Us - " + AppVar.Name,
                "Contact Us, Feedback about " + AppVar.Name);


            if (ModelState.IsValid) {
                Db.Entry(feedback).State = EntityState.Added;
                Db.SaveChanges();
                AppVar.SetSavedStatus(ViewBag);
                //send a email.
                AppVar.Mailer.NotifyAdmin("A feedback has been added by " + feedback.Email,
                    "Please check your feedback inbox. Feedback :<br>" + feedback.Message);

                return View(feedback);
            }

            AppVar.SetErrorStatus(ViewBag);
            return View(feedback);
        }
    }
}