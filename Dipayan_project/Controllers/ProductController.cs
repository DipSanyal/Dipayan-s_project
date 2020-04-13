using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Dipayan_project.Models;
using PagedList;
using PagedList.Mvc;

namespace Dipayan_project.Controllers
{
    
    public class ProductController : Controller
    {
        
        private ProductEntities db = new ProductEntities();
        //
        // GET: /Product/
        [Route("Product/Add")]
        public ActionResult Add()
        {
            ///*var item = from c in db.category
            //           select c.CategoryId;*/
            //var item1 = from c in db.category
            //           select c.CategoryName;
            //ViewBag.use = item1;
            ////ViewBag.use1 = item1;
            List<Category> c = db.category.ToList<Category>();
            ViewBag.CategoryList = new SelectList(c, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Save(Product p)
        {
            Product p1 = new Product();
            p1.ProductName = p.ProductName;
            p1.category = db.category.SingleOrDefault<Category>(m => m.CategoryId == p.CategoryId);
           // p1.isprodchecked = false;
           // p1.category = p.category;
            p1.CategoryId = p.CategoryId;
           // p1.CategoryId = p.category.CategoryId;
            if (ModelState.IsValid)
            {
                try
                {
                    db.product.Add(p1);
                    db.SaveChanges();
                    return RedirectToAction("GetData");
                }
                catch
                {
                    return RedirectToAction("Add");
                }
            }
            else
            {
                ModelState.AddModelError("", "All fields are required");
                return RedirectToAction("Add");
            }
        }
        [Route("Product/GetData")]
        public ActionResult GetData(int? page)
        {
            


           // var c = db.product.ToList<Product>().ToPagedList(page?? 1,10);
           Product i = new Product();
           var pro = db.product.ToList<Product>().ToPagedList(page ?? 1,4);
           var cat = db.category.ToList<Category>();
        
               return View(pro);
        }
        [Route("Product/Edit/name")]
        [HttpGet]
        public ActionResult Edit(string name)
        {

            var c1 = db.product.SingleOrDefault<Product>(m => m.ProductName == name);
            
            if (c1 != null)
                return View(c1);
            else
                return RedirectToAction("GetData");
        }
        [HttpPost]
        public ActionResult editdata(Product c)
        {
            var c1 = db.product.SingleOrDefault<Product>(m => m.ProductId == c.ProductId);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "All fields are required");
                return View("Edit");
            }
            if (TryUpdateModel(c1))
            {
                db.SaveChanges();
                return RedirectToAction("GetData");
            }

            return RedirectToAction("GetData");


        }
        [Route("Product/DeleteRecord/name")]
        [HttpGet]
        public ActionResult DeleteRecord(string name)
        {
            var c1 = db.product.SingleOrDefault<Product>(m => m.ProductName == name);
            if (c1 != null)
                return View(c1);
            else
                return RedirectToAction("GetData");
        }
        [HttpPost]
        public ActionResult delete(Product c)
        {
            var c1 = db.product.SingleOrDefault<Product>(m => m.ProductId == c.ProductId && m.ProductName == c.ProductName);
            if (c1 != null)
            {
                try
                {
                    db.product.Remove(c1);
                    db.SaveChanges();
                    return RedirectToAction("GetData");
                }
                catch
                {
                    return RedirectToAction("GetData");
                }
            }
            else
                return RedirectToAction("GetData");




        }
	}
}