﻿using System;
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

        //
        // GET: /Category/

        public ActionResult Index()
        {
            IEnumerable<Category> categories = categoryService.GetAll();
            if (categories == null || categories.Count() == 0)
            {
                return HttpNotFound();
            }

            return View(categories);
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
                categoryService.Edit(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Category category = categoryService.FindSingleBy(id);
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