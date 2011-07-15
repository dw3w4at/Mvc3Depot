using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc3Depot.Models;

namespace Mvc3Depot.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private Mvc3DepotContext db = new Mvc3DepotContext();
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            @ViewBag.OrderCount = db.Orders.Count();
            return View();
        }

    }
}
