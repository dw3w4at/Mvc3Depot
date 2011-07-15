using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc3Depot.Models;

namespace Mvc3Depot.Controllers
{
    public class HomeController : Controller
    {
        private Mvc3DepotContext db = new Mvc3DepotContext();

        //
        // GET: /Home/Index

        public ActionResult Index()
        {
            Guid currentCartId = ExtentionMethods.GetCartId(this);
            var products = db.Products.OrderBy(p => p.Title);
            var lineItems = db.LineItems.Include(l => l.Product).Where(l => l.CartId == currentCartId);
            ViewBag.Cart = lineItems;
            return View(products.ToList());
        }
    }
}
