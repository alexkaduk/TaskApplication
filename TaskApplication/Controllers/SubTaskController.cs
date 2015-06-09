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
    public class SubTaskController : Controller
    {

        private SubTaskService subTaskService = new SubTaskService();

        public ActionResult _Index(int issueId, bool isEdit = true)
        {
            ViewBag.IssueId = issueId;
            ViewBag.IsEdit = isEdit;
            var subTasks = subTaskService.GetAllByIssueId(issueId);
            return PartialView("_Index", subTasks);
        }
        //
        // GET: /SubTask

       /* private TaskContex db = new TaskContex();

        //
        // GET: /SubTask

        public ActionResult _Index(int issueId, bool isEdit = true)
        {
            ViewBag.IssueId = issueId;
            ViewBag.IsEdit = isEdit;
            var subTasks = db.SubTasks.Where(s => s.IssueId == issueId).ToList();
            return PartialView("_Index", subTasks);
        }

        public PartialViewResult _ChangeSubTaskStatus(int issueId, int subTaskId)
        {
            var subTask = db.SubTasks.Where(s => s.SubTaskId == subTaskId).FirstOrDefault();

            if (subTask.StatusId == (int)Statuses.Open)
            {
                subTask.StatusId = (int)Statuses.Resolved;
            }
            else if (subTask.StatusId == (int)Statuses.Resolved)
            {
                subTask.StatusId = (int)Statuses.Open;
            }

            db.SaveChanges();

            var issue = db.Issues.Find(issueId);
            if (issue.SubTasks.All(s => s.StatusId == (int)Statuses.Resolved))
            {
                issue.StatusId = (int)Statuses.Resolved;
                db.SaveChanges();
                ViewBag.IsIssueShouldBeUpdated = true;
            }

            var subTasks = db.SubTasks.Where(s => s.IssueId == issueId).ToList();
            return PartialView("_Index", subTasks);
        }

        public PartialViewResult _GetSubTasks(int issueId)
        {
            ViewBag.IssueId = issueId;
            var subTasks = db.SubTasks.Where(s => s.IssueId == issueId).ToList();

            return PartialView("_GetSubTasks", subTasks);
        }

        [ChildActionOnly]
        public PartialViewResult _SubTaskForm(int id)
        {
            ViewBag.Statuses = db.Statuses;

            var model = new SubTask { IssueId = id };
            return PartialView("_SubTaskForm", model);
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _SubmitSubTask(SubTask subTask)
        {
            db.SubTasks.Add(subTask);
            db.SaveChanges();

            ViewBag.IssueId = subTask.IssueId;
            var subTasks = db.SubTasks.Where(s => s.IssueId == subTask.IssueId).ToList();
            return PartialView("_GetSubTasks", subTasks);
        }


        public ActionResult Index()
        {
            return View(db.SubTasks.ToList());
        }

        //
        // GET: /SubTask/Details/5

        public ActionResult Details(int id = 0)
        {
            SubTask subtask = db.SubTasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            return View(subtask);
        }

        //
        // GET: /SubTask/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SubTask/Create

        [HttpPost]
        public ActionResult Create(SubTask subtask)
        {
            if (ModelState.IsValid)
            {
                db.SubTasks.Add(subtask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subtask);
        }

        //
        // GET: /SubTask/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Statuses = db.Statuses;

            SubTask subtask = db.SubTasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }

            return View(subtask);
        }

        //
        // POST: /SubTask/Edit/5

        [HttpPost]
        public ActionResult Edit(SubTask subtask)
        {
            var oldSubtask = db.SubTasks.Find(subtask.SubTaskId);
            subtask.IssueId = oldSubtask.IssueId;
            db.Entry(oldSubtask).State = EntityState.Detached;

            if (ModelState.IsValid)
            {
                db.Entry(subtask).State = EntityState.Modified;
                db.SaveChanges();

                var issue = db.Issues.Find(subtask.IssueId);
                if (issue.SubTasks.All(s => s.StatusId == (int)Statuses.Resolved))
                {
                    issue.StatusId = (int)Statuses.Resolved;
                    db.SaveChanges();
                }
                
                return RedirectToAction("Edit", "Issue", new { id = subtask.IssueId });
            }
            return View(subtask);
        }

        //
        // GET: /SubTask/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SubTask subtask = db.SubTasks.Find(id);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            return View(subtask);
        }

        //
        // POST: /SubTask/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SubTask subtask = db.SubTasks.Find(id);
            var issueId = subtask.IssueId;
            db.SubTasks.Remove(subtask);
            db.SaveChanges();
            return RedirectToAction("Edit", "Issue", new { id = issueId });
        }*/
    }
}