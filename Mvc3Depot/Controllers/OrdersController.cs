using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc3Depot.Models;
using PagedList;

namespace Mvc3Depot.Controllers
{ 
    public class OrdersController : Controller
    {
        private Mvc3DepotContext db = new Mvc3DepotContext();
        private const int PAGE_SIZE = 10;

        //
        // GET: /Orders/?page=1
        [Authorize]
        public ViewResult Index(int? page)
        {
            var pageIndex = page ?? 1;
            return View(db.Orders.OrderBy(o => o.OrderId).ToPagedList(pageIndex - 1, PAGE_SIZE));
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            Guid currentCartId = ExtentionMethods.GetCartId(this);
            var lineItems = db.LineItems.Include(l => l.Product).Where(l => l.CartId == currentCartId);
            if (lineItems.Count() > 0)
            {
                ViewBag.Cart = lineItems;
                ViewBag.PayType = new SelectList(getAllPaymentTypes(), "Key", "Value");
                return View();
            }
            else
            {
                TempData["Notice"] = "Your cart is empty.";
                return RedirectToAction("Index", "Home");
            }
        } 

        //
        // POST: /Orders/Create

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            Guid currentCartId = ExtentionMethods.GetCartId(this);
            var lineItems = db.LineItems.Include(l => l.Product).Where(l => l.CartId == currentCartId);
            order.LineItems = lineItems.ToList();
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                ExtentionMethods.DeleteCartId(this);
                TempData["Notice"] = "Thank you for your order.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Cart = lineItems;
                ViewBag.PayType = new SelectList(getAllPaymentTypes(), "Key", "Value");
                return View(order);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private IDictionary<int, string> getAllPaymentTypes()
        {
            IDictionary<int, string> dictionary = new Dictionary<int, string>();
            foreach (int payType in Enum.GetValues(typeof(PaymentType)))
            {
                dictionary[payType] = Enum.GetName(typeof(PaymentType), payType);
            }
            return dictionary;
        }
    }
}