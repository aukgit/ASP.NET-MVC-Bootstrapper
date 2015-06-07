#region using block

using System;
using System.Linq;
using System.Web.Mvc;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevBootstrapper.Modules.Cache;

#endregion

namespace DevBootstrapper.Areas.Admin.Controllers {
    public class CountriesController : Controller {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index() {
            return View(CachedQueriedData.GetCountries().ToList());
        }

        public ActionResult Edit(Int32 id) {
            var zones = CachedQueriedData.GetTimezones(id);
            ViewBag.Timezone = new SelectList(_db.UserTimeZones.ToList(), "UserTimeZoneID", "Display");
            ViewBag.CountryID = id;
            ViewBag.CountryName = _db.Countries.Find(id).DisplayCountryName + " - " + _db.Countries.Find(id).Alpha2Code;
            return View(zones);
        }

        public ActionResult Delete(Int32 id) {
            var timezone = _db.UserTimeZones.Find(id);
            _db.UserTimeZones.Remove(timezone);
            _db.SaveChanges();
            return RedirectToActionPermanent("Edit", new {id});
        }

        [HttpPost]
        public ActionResult Edit(int countryId, int timezone, bool hasMultiple) {
            var country = _db.Countries.Find(countryId);

            var foundTimeZone = _db.UserTimeZones.Find(timezone);
            if (foundTimeZone != null) {
                var addRelation = new CountryTimezoneRelation {
                    CountryId = country.CountryId,
                    UserTimeZoneId = foundTimeZone.UserTimeZoneId
                };
                var anyExist =
                    _db.CountryTimezoneRelations.Any(
                        n => n.UserTimeZoneId == addRelation.UserTimeZoneId && n.CountryId == addRelation.CountryId);

                if (!anyExist) {
                    //not exist then add
                    _db.CountryTimezoneRelations.Add(addRelation);
                    country.RelatedTimeZoneId = addRelation.UserTimeZoneId;
                }

                country.IsSingleTimeZone = !hasMultiple;
                country.RelatedTimeZoneId = addRelation.UserTimeZoneId;
                _db.SaveChanges();
            }
            var zones = CachedQueriedData.GetTimezones(countryId);
            ViewBag.Timezone = new SelectList(_db.UserTimeZones.ToList(), "UserTimeZoneID", "Display");
            ViewBag.CountryID = countryId;
            ViewBag.CountryName = _db.Countries.Find(countryId).DisplayCountryName + " - " +
                                  _db.Countries.Find(countryId).Alpha2Code;

            return View(zones);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}