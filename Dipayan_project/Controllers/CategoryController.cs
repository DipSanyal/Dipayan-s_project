using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dipayan_project.Models;
using Dipayan_project.View_Models;
using System.ComponentModel.DataAnnotations;
using PagedList;
using PagedList.Mvc;

namespace Dipayan_project.Controllers
{
    public class CategoryController : Controller
    {
        private ProductEntities db = new ProductEntities();
        //
        // GET: /Category/
        //[Route("Category/Add")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(Category c)
        {
            Category c1 = new Category();
            c1.CategoryName = c.CategoryName;
            var data = db.category.FirstOrDefault<Category>(m => m.CategoryName == c.CategoryName);
            if (data == null)
            {
                //c1.iscatchecked= false;
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.category.Add(c1);
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
                    ModelState.AddModelError("","All fields are required");
                    return View("Add");
                }
            }
            else
            {
                return RedirectToAction("Add");
            }
        }
        [Route("Category/GetData")]
        public ActionResult GetData(int? page)
        {
           


            var c = db.category.ToList<Category>().ToPagedList(page ?? 1, 3); ;
          
            
            return View(c);
        }
        [Route("Category/Edit/name")]
        [HttpGet]
        public ActionResult Edit(string name)
        {
            
            var c1 = db.category.SingleOrDefault<Category>(m => m.CategoryName == name);
            if (c1 != null)
                return View(c1);
            else
                return RedirectToAction("GetData");
        }
        [HttpPost]
        public ActionResult editdata(Category c)
        {
            var c1 = db.category.SingleOrDefault<Category>(m => m.CategoryId == c.CategoryId);
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "All fields are required");
                return View("Edit");
            }
            if(TryUpdateModel(c1))
            {
                db.SaveChanges();
                return RedirectToAction("GetData");
            }

            return RedirectToAction("GetData");
            
            
        }
        [Route("Category/DeleteRecord/name")]
        [HttpGet]
        public ActionResult DeleteRecord(string name)
        {
            var c1 = db.category.SingleOrDefault<Category>(m => m.CategoryName == name);
            if (c1 != null)
                return View(c1);
            else
                return RedirectToAction("GetData");
        }
        [HttpPost]
        public ActionResult delete(Category c)
        {
            var c1 = db.category.SingleOrDefault<Category>(m => m.CategoryId == c.CategoryId && m.CategoryName==c.CategoryName);
            if(c1!=null)
            {
                try
                {
                    db.category.Remove(c1);
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
        //[HttpPost]
        //public ActionResult editdata()
        //{

        //}
	}
}