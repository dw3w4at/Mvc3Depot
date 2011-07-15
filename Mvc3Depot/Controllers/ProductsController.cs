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
    public class ProductsController : Controller
    {
        private Mvc3DepotContext db = new Mvc3DepotContext();

        //
        // GET: /Products/

        public ViewResult Index()
        {
            return View(db.Products.ToList());
        }

        //
        // GET: /Products/Details/5

        public ActionResult Details(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                TempData["Notice"] = "Invalid product";
                return RedirectToAction("Index");  
            }
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Products/Create

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(product);
        }
        
        //
        // GET: /Products/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }

        //
        // PUT: /Products/Edit/5

        [HttpPut, ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Products/Delete/5
 
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }

        //
        // DELETE: /Products/Delete/5

        [HttpDelete, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}