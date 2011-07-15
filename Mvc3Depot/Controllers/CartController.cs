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
    public class CartController : Controller
    {
        private Mvc3DepotContext db = new Mvc3DepotContext();

        //
        // POST: /Cart/Create

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(LineItem lineitem)
        {
            lineitem.CartId = ExtentionMethods.GetCartId(this);
            if (ModelState.IsValid)
            {
                var currentLineItem = db.LineItems.AsEnumerable().Where(l => l.Equals(lineitem)).SingleOrDefault();
                if (currentLineItem != null)
                {
                    currentLineItem.Quantity++;
                    db.Entry(currentLineItem).State = EntityState.Modified;
                }
                else
                {
                    lineitem.Quantity = 1;
                    db.LineItems.Add(lineitem);
                    currentLineItem = lineitem;
                }
                db.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    var lineitems = db.LineItems.Include(l => l.Product).Where(l => l.CartId == lineitem.CartId);
                    ViewBag.CurrentItem = currentLineItem;
                    return PartialView("_Cart", lineitems);
                }
                else
                {
                    return RedirectToAction("Index", "Home");  
                }
            }
            else
            {
                TempData["Notice"] = "Model state is invalid.";
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // DELETE: /Cart/Empty

        [HttpDelete, ValidateAntiForgeryToken]
        public ActionResult Empty()
        {
            Guid currentCartId = ExtentionMethods.GetCartId(this);
            var lineItems = db.LineItems.Where(l => l.CartId == currentCartId);
            foreach (var lineitem in lineItems)
            {
                db.LineItems.Remove(lineitem);
            }
            db.SaveChanges();
            ExtentionMethods.DeleteCartId(this);
            TempData["Notice"] = "Your cart is currently empty.";
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}