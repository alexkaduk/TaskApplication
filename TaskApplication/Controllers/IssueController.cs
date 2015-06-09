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
        //private TaskContex db = new TaskContex();
        private IssueService issueService = new IssueService();
        private CategoryService categoryService = new CategoryService();
        private StatusService statusService = new StatusService();

        //
        // GET: /Issue/

        public ActionResult Index()
        {
            /*   ViewBag.Statuses = db.Statuses;

               if (!issueService.IsAnyResolved)
               {
                   ViewBag.isAnyResolvedIssue = "Delete all resolved issues (nothing to do)";
               }

               return View(db.Issues.OrderBy(i => i.CategoryId).ToList());*/
            //return View(new List<Issue>());

            ViewBag.Statuses = statusService.GetAll();

            if (!issueService.IsAnyResolved())
            {
                ViewBag.isAnyResolvedIssue = "Delete all resolved issues (nothing to do)";
            }

            return View(issueService.GetAll());
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
            /*ViewBag.Categories = db.Categories;
            ViewBag.Statuses = db.Statuses;
            */

            ViewBag.Categories = categoryService.GetAll();
            ViewBag.Statuses = statusService.GetAll();

            return View();
        }

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(Issue issue)
        {
            /*   issue.IssueCreateDate = DateTime.Now;
               issue.IssueUpdateDate = DateTime.Now;

               issue.StatusId = db.Statuses.First(s => s.StatusName == "Open").StatusId;
               if (ModelState.IsValid)
               {
                   db.Issues.Add(issue);
                   db.SaveChanges();
                   return RedirectToAction("Index");
               }

               return View(issue);
               */
            //return View(new Issue());

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
            /*
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.Categories;
            ViewBag.Statuses = db.Statuses;
            return View(issue);
            */
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
            /*
            Issue oldIssue = db.Issues.Find(issue.IssueId);
            db.Entry(oldIssue).State = EntityState.Detached;

            issue.IssueCreateDate = oldIssue.IssueCreateDate;
            issue.IssueUpdateDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(issue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
            */

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
            /*
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
            */
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
            /*
            Issue issue = db.Issues.Find(id);
            if (db.SubTasks.Any(s => s.IssueId == id))
            {
                ViewBag.ErrorMessage = "Cannot delete. Issue is used.";
                return View(issue);
            }
            else
            {
                db.Issues.Remove(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            */

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
            /*
            var resovledIssues = db.Issues.Where(i => i.StatusId == (int)Statuses.Resolved).ToList();
            if (resovledIssues == null)
            {
                return HttpNotFound();
            }
            return View(resovledIssues);
             * */
            return View(new Issue());
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost, ActionName("DeleteResolved")]
        public ActionResult DeleteResolvedConfirmed()
        {
            /*var resovledIssues = db.Issues.Where(i => i.StatusId == (int)Statuses.Resolved).ToList();
            foreach (Issue issue in resovledIssues)
            {
                foreach (var subtask in issue.SubTasks.ToArray())
                {
                    db.SubTasks.Remove(subtask);
                }
                db.Issues.Remove(issue);
            }
            db.SaveChanges();*/
            return RedirectToAction("Index");
        }
    }
}