using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevBootstrapper.Controllers;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.EntityModel.POCO;
using DevTrends.MvcDonutCaching;

namespace DevBootstrapper.Controllers
{
    [DonutOutputCache(CacheProfile = "YearNoParam")]
    public class PartialsController : GenericController<MagazineEntities> {

    }
	
}
