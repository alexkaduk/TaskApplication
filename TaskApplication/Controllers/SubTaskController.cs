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
    public class SubTaskController : Controller
    {

        //private SubTaskService _subTaskService = new SubTaskService();
        //private IssueService _issueService = new IssueService();
        //private StatusService _statusService = new StatusService();

        private readonly ISubTaskService _subTaskService = Ioc.Get<ISubTaskService>();
        private readonly IIssueService _issueService = Ioc.Get<IIssueService>();
        private readonly IStatusService _statusService = Ioc.Get<IStatusService>();

        public ActionResult _Index(int issueId, bool isEdit = true)
        {
            ViewBag.IssueId = issueId;
            ViewBag.IsEdit = isEdit;
            var subTasks = _subTaskService.GetAllByIssueId(issueId);
            return PartialView("_Index", subTasks);
        }

        public PartialViewResult _ChangeSubTaskStatus(int issueId, int subTaskId)
        {
            var subTask = _subTaskService.FindSingleBy(subTaskId, false);

            _subTaskService.ChangeStatusOpenResolve(subTask);

            var issue = _issueService.FindSingleBy(issueId);

            ViewBag.IsIssueShouldBeUpdated = _issueService.ChangeStatusIfAllSubTasksResolved(issue);

            var subTasks = _subTaskService.GetAllByIssueId(issueId);

            return PartialView("_Index", subTasks);
        }

        public PartialViewResult _GetSubTasks(int issueId)
        {
            ViewBag.IssueId = issueId;

            var subTasks = _subTaskService.GetAllByIssueId(issueId);

            return PartialView("_GetSubTasks", subTasks);
        }

        [ChildActionOnly]
        public PartialViewResult _SubTaskForm(int id)
        {
            ViewBag.Statuses = _statusService.GetAll();

            var model = new SubTask { IssueId = id };
            return PartialView("_SubTaskForm", model);
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _SubmitSubTask(SubTask subTask)
        {
            _subTaskService.Add(subTask);

            ViewBag.IssueId = subTask.IssueId;

            var subTasks = _subTaskService.GetAllByIssueId(subTask.IssueId);

            return PartialView("_GetSubTasks", subTasks);
        }

        //
        // GET: /SubTask/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Statuses = _statusService.GetAll();

            SubTask subtask = _subTaskService.FindSingleBy(id, false);
            if (subtask == null)
            {
                return HttpNotFound();
            }

            return View(subtask);
        }

        //
        // PUT: /SubTask/Edit/5
        [HttpPut]
        public ActionResult Edit(SubTask subtask)
        {
            if (ModelState.IsValid)
            {
                _subTaskService.Edit(subtask);
                return RedirectToAction("Edit", "Issue", new { id = subtask.IssueId });
            }
            return View(subtask);
        }

        //
        // GET: /SubTask/Delete/5
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            SubTask subtask = _subTaskService.FindSingleBy(id, false);
            if (subtask == null)
            {
                return HttpNotFound();
            }
            return View(subtask);
        }

        //
        // Delete: /SubTask/Delete/5
        [HttpDelete, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var issueId = _subTaskService.FindSingleBy(id, false).IssueId;

            _subTaskService.Delete(id);
            return RedirectToAction("Edit", "Issue", new { id = issueId });
        }

        //
        // GET: /SubTask

        /* private TaskContex db = new TaskContex();

         //
         // GET: /SubTask

         //public ActionResult _Index(int issueId, bool isEdit = true)
         {
             ViewBag.IssueId = issueId;
             ViewBag.IsEdit = isEdit;
             var subTasks = db.SubTasks.Where(s => s.IssueId == issueId).ToList();
             return PartialView("_Index", subTasks);
         }

         //public PartialViewResult _ChangeSubTaskStatus(int issueId, int subTaskId)
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

         //public PartialViewResult _GetSubTasks(int issueId)
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

////
//// GET: /SubTask
//[HttpGet]
//public ActionResult Index()
//{
//    return View(_subTaskService.GetAll());
//}

////
//// GET: /SubTask/Details/5
//[HttpGet]
//public ActionResult Details(int id = 0)
//{
//    SubTask subtask = _subTaskService.FindSingleBy(id);
//    if (subtask == null)
//    {
//        return HttpNotFound();
//    }
//    return View(subtask);
//}

////
//// GET: /SubTask/Create
//[HttpGet]
//public ActionResult Create()
//{
//    return View();
//}

////
//// POST: /SubTask/Create
//[HttpPost]
//public ActionResult Create(SubTask subtask)
//{
//    if (ModelState.IsValid)
//    {
//        _subTaskService.Add(subtask);
//        return RedirectToAction("Index");
//    }

//    return View(subtask);
//}