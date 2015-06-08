using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskApplication.DataAccess.Entities;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.Models;
using TaskApplication.Services.Concrete;

namespace TaskApplication.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService = new CategoryService();
        //private CategoryReposiltory categoryReposiltory = new CategoryReposiltory();
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View(categoryService.GetAll());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            Category category = categoryService.FindSingleBy(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Add(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = categoryService.FindSingleBy(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(category).State = EntityState.Modified;
                categoryService.Edit(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category category = categoryService.FindSingleBy(id); // .Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Category category = categoryReposiltory.FindSingleBy(c => c.CategoryId == id);
            if (categoryService.IsUsed(id))
            {
                ViewBag.ErrorMessage = "Cannot delete. Category is used.";
                return View(categoryService.FindSingleBy(id));
            }
            else
            {
                categoryService.Delete(id);
                return RedirectToAction("Index");
            }
        }
    }
}