using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskApplication.Models;

namespace TaskApplication.Controllers
{
    public class SubTaskController : Controller
    {
        private TaskContex db = new TaskContex();

        //
        // GET: /SubTask

        public ActionResult _Index(int issueId)
        {
            ViewBag.IssueId = issueId;
            var subTasks = db.SubTasks.Where(s => s.IssueId == issueId).ToList();
            return PartialView("_Index", subTasks);
        }
        int counter = 1;
        public void _ChangeSubTaskStatus(int subTaskId)
        {   
            counter++;
            ViewBag.counteer = "2+ counter"; // чому сцуко не передається на вью ???????????

            if (db.SubTasks.Where(s => s.SubTaskId == subTaskId).FirstOrDefault().StatusId.ToString() == "1")
                db.SubTasks.Where(s => s.SubTaskId == subTaskId).FirstOrDefault().StatusId = 2;
            if (db.SubTasks.Where(s => s.SubTaskId == subTaskId).FirstOrDefault().StatusId.ToString() == "2")
                db.SubTasks.Where(s => s.SubTaskId == subTaskId).FirstOrDefault().StatusId = 1;
            db.SaveChanges();
            _Index(db.SubTasks.Where(s => s.SubTaskId == subTaskId).FirstOrDefault().IssueId);
            
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
            if (ModelState.IsValid)
            {
                db.Entry(subtask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            db.SubTasks.Remove(subtask);
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