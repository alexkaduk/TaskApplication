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
    public class IssueController : Controller
    {
        //private IssueService issueService = new IssueService();
        //private CategoryService categoryService = new CategoryService();
        //private StatusService statusService = new StatusService();

        private readonly IIssueService _issueService = Ioc.Get<IIssueService>();
        private readonly ICategoryService _categoryService = Ioc.Get<ICategoryService>();
        private readonly IStatusService _statusService = Ioc.Get<IStatusService>();
        
        //
        // GET: /Issue/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Statuses = _statusService.GetAll();

            if (!_issueService.IsAnyResolved())
            {
                ViewBag.isAnyResolvedIssue = "Delete all resolved issues (nothing to do)";
            }

            IEnumerable<Issue> issues = _issueService.GetAll();
            if (issues == null || issues.Count() == 0)
            {
                return HttpNotFound();
            }

            return View(issues);
        }

        //
        // GET: /Issue/Details/5
        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            Issue issue = _issueService.FindSingleBy(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // GET: /Issue/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Statuses = _statusService.GetAll();

            return View();
        }

        //
        // POST: /Issue/Create
        [HttpPost]
        public ActionResult Create(Issue issue)
        {
            if (ModelState.IsValid)
            {
                _issueService.Add(issue);
                return RedirectToAction("Index");
            }

            return View(issue);
        }

        //
        // GET: /Issue/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Categories = _categoryService.GetAll();
            ViewBag.Statuses = _statusService.GetAll();

            Issue issue = _issueService.FindSingleBy(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // PUT: /Issue/Edit/5
        [HttpPut]
        public ActionResult Edit(Issue issue)
        {
            if (ModelState.IsValid)
            {
                _issueService.Edit(issue);
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        //
        // GET: /Issue/Delete/5
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            Issue issue = _issueService.FindSingleBy(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // DELETE: /Issue/Delete/5
        [HttpDelete, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_issueService.IsUsed(id))
            {
                ViewBag.ErrorMessage = "Cannot delete. Issue is used.";
                return View(_issueService.FindSingleBy(id));
            }
            else
            {
                _issueService.Delete(id);
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Issue/DeleteResolved/
        [HttpGet]
        public ActionResult DeleteResolved()
        {

            var resovledIssues = _issueService.GetAllResolved();
            if (resovledIssues == null)
            {
                return HttpNotFound();
            }
            return View(resovledIssues);
        }

        //
        // DELETE: /Issue/Delete/5
        //[HttpDelete, ActionName("DeleteResolved")]
        [HttpPost, ActionName("DeleteResolved")]
        public ActionResult DeleteResolvedConfirmed()
        {
            _issueService.DeleteAllResolved();

            return RedirectToAction("Index");
        }
    }
}