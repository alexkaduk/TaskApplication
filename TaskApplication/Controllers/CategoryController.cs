using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskApplication.Common;
using TaskApplication.DataAccess.Entities;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.Models;
using TaskApplication.Services.Concrete;
using TaskApplication.Services.Interfaces;

namespace TaskApplication.Controllers
{
    public class CategoryController : Controller
    {
        //private CategoryService categoryService = new CategoryService();
        private readonly ICategoryService _categoryService = Ioc.Get<ICategoryService>();

        //
        // GET: /Category/
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Category> categories = _categoryService.GetAll();
            if (categories == null || categories.Count() == 0)
            {
                return HttpNotFound();
            }

            return View(categories);
        }

        //
        // GET: /Category/Details/5
        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            Category category = _categoryService.FindSingleBy(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create
        [HttpGet]
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
                _categoryService.Add(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /Category/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Category category = _categoryService.FindSingleBy(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // PUT: /Category/Edit/5
        [HttpPut]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Edit(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            Category category = _categoryService.FindSingleBy(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // DELETE: /Category/Delete/5
        [HttpDelete, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_categoryService.IsUsed(id))
            {
                ViewBag.ErrorMessage = "Cannot delete. Category is used.";
                return View(_categoryService.FindSingleBy(id));
            }
            else
            {
                _categoryService.Delete(id);
                return RedirectToAction("Index");
            }
        }
    }
}