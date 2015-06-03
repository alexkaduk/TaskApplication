using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskApplication.Models;

namespace TaskApplication.Controllers
{
    public class IssueController : Controller
    {
        private TaskContex db = new TaskContex();

        //
        // GET: /Issue/

        public ActionResult Index()
        {
            ViewBag.Statuses = db.Statuses;
            return View(db.Issues.OrderBy(i => i.CategoryId).ToList());
        }

        //
        // GET: /Issue/Details/5

        public ActionResult Details(int id = 0)
        {
            Issue issue = db.Issues.Find(id);
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
            ViewBag.Categories = db.Categories;
            ViewBag.Statuses = db.Statuses;

            return View();
        }

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(Issue issue)
        {
            issue.IssueCreateDate = DateTime.Now;
            issue.IssueUpdateDate = DateTime.Now;

            issue.StatusId = db.Statuses.First(s => s.StatusName == "Open").StatusId;
            if (ModelState.IsValid)
            {
                db.Issues.Add(issue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(issue);
        }

        //
        // GET: /Issue/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.Categories;
            ViewBag.Statuses = db.Statuses;
            return View(issue);
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(Issue issue)
        {
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
        }

        //
        // GET: /Issue/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Issue issue = db.Issues.Find(id);
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
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteResolved()
        {
            var resovledIssue = db.Issues.Where(i => i.StatusId == (int)Statuses.Resolved).ToList(); 
            if (resovledIssue == null)
            {
                return HttpNotFound();
            }
            return View(resovledIssue);
        }

        //
        // POST: /Issue/Delete/5

        [HttpPost, ActionName("DeleteResolved")]
        public ActionResult DeleteResolvedConfirmed()
        {
            var resovledIssue = db.Issues.Where(i => i.StatusId == (int)Statuses.Resolved).ToList();
            foreach (Issue issue in resovledIssue)
            {
                // якщо є підзадачі, то не можна видаляти - зробити аналогічно категоріям!!!
                db.Issues.Remove(issue);
            }
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