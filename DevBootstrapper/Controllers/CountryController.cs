#region using block

using System.Linq;
using System.Web.Mvc;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevBootstrapper.Modules.Cache;
using DevBootstrapper.Modules.InternetProtocolRelations;

#endregion

namespace DevBootstrapper.Controllers {
    public class CountryController : Controller {
        public ActionResult Index() {
            return View();
        }

        /// <summary>
        /// </summary>
        /// <param name="id">ip address</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string id) {
            ViewBag.id = id;
            // TODO : follow the code and implement in your application.
            //var countries = CachedQueriedData.GetCountries();
            //var countryId = IpConfigRelations.GetCountryId(id);
            Country country = null;

            var value = IpConfigRelations.IpToValue(id);
            using (var db = new ApplicationDbContext()) {
                //SELECT * FROM [ip-to-country] WHERE (([BeginingIP] <= ?) AND ([EndingIP] >= ?))
                var countryIp = db.CountryDetectByIPs.FirstOrDefault(n => n.BeginingIP <= value && n.EndingIP >= value);
                if (countryIp != null) {
                    country = CachedQueriedData.GetCountries().FirstOrDefault(n =>
                        n.CountryID == countryIp.CountryID
                        );
                    if (country != null) {
                        return View(country);
                    }
                }
            }
            return View();
            //return HtmlHelpers.DropDownCountry(countries);
        }
    }
}