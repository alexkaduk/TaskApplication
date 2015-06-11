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
    public class IssueController : Controller
    {
        private IssueService issueService = new IssueService();
        private CategoryService categoryService = new CategoryService();
        private StatusService statusService = new StatusService();

        //
        // GET: /Issue/

        public ActionResult Index()
        {
            ViewBag.Statuses = statusService.GetAll();

            if (!issueService.IsAnyResolved())
            {
                ViewBag.isAnyResolvedIssue = "Delete all resolved issues (nothing to do)";
            }

            IEnumerable<Issue> issues = issueService.GetAll();
            if (issues == null || issues.Count() == 0)
            {
                return HttpNotFound();
            }

            return View(issues);
        }

        //
        // GET: /Issue/Details/5

        public ActionResult Details(int id = 0)
        {
            Issue issue = issueService.FindSingleBy(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // GET: /Issue/Create

        public ActionResult Create()
        {
            ViewBag.Categories = categoryService.GetAll();
            ViewBag.Statuses = statusService.GetAll();

            return View();
        }

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(Issue issue)
        {
            if (ModelState.IsValid)
            {
                issueService.Add(issue);
                return RedirectToAction("Index");
            }

            return View(issue);
        }

        //
        // GET: /Issue/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Categories = categoryService.GetAll();
            ViewBag.Statuses = statusService.GetAll();

            Issue issue = issueService.FindSingleBy(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(Issue issue)
        {
            if (ModelState.IsValid)
            {
                issueService.Edit(issue);
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        //
        // GET: /Issue/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Issue issue = issueService.FindSingleBy(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (issueService.IsUsed(id))
            {
                ViewBag.ErrorMessage = "Cannot delete. Issue is used.";
                return View(issueService.FindSingleBy(id));
            }
            else
            {
                issueService.Delete(id);
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteResolved()
        {

            var resovledIssues = issueService.GetAllResolved();
            if (resovledIssues == null)
            {
                return HttpNotFound();
            }
            return View(resovledIssues);
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost, ActionName("DeleteResolved")]
        public ActionResult DeleteResolvedConfirmed()
        {
            issueService.DeleteAllResolved();

            return RedirectToAction("Index");
        }
    }
}