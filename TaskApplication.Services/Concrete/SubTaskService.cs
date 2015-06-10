using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.Services.Interfaces;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.DataAccess.Entities;
using log4net;

namespace TaskApplication.Services.Concrete
{
    public class SubTaskService : ISubTaskService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(SubTaskService));

        // private CategoryReposiltory categoryReposiltory = new CategoryReposiltory();
        private IssueReposiltory issueReposiltory = new IssueReposiltory();
        private SubTaskReposiltory subTaskReposiltory = new SubTaskReposiltory();

        public IEnumerable<SubTask> GetAll()
        {
            return subTaskReposiltory.GetAll();
        }

        public IEnumerable<SubTask> GetAllByIssueId(int id)
        {
            return subTaskReposiltory.FindBy(s => s.IssueId == id).ToList();
        }

        public SubTask FindSingleBy(int id, bool isDetached = false)
        {
            return subTaskReposiltory.FindSingleBy(s => s.SubTaskId == id, isDetached);
        }

        public void ChangeStatusOpenResolve(SubTask subTask)
        {
            try
            {
                if (subTask.StatusId == (int)Statuses.Open)
                {
                    subTask.StatusId = (int)Statuses.Resolved;
                }
                else if (subTask.StatusId == (int)Statuses.Resolved)
                {
                    subTask.StatusId = (int)Statuses.Open;
                }

                subTaskReposiltory.Save();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public void Add(SubTask subTask)
        {
            subTaskReposiltory.Add(subTask);
            subTaskReposiltory.Save();
        }

        public void Edit(SubTask subTask)
        {
            /*
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
             */
            /*
             Issue oldIssue = issueReposiltory.FindSingleBy(i => i.IssueId == issue.IssueId);
            // db.Entry(oldIssue).State = EntityState.Detached;

            issue.IssueCreateDate = oldIssue.IssueCreateDate;
            issue.IssueUpdateDate = DateTime.Now;

            issueReposiltory.Edit(issue);
            issueReposiltory.Save();
             */
            var oldSubtask = FindSingleBy(subTask.SubTaskId, true);
            subTask.IssueId = oldSubtask.IssueId;
            subTaskReposiltory.Edit(subTask);
            subTaskReposiltory.Save();

            var issue = issueReposiltory.FindSingleBy(i => i.IssueId == subTask.IssueId);

            if (!issue.SubTasks.Where(s => s.StatusId == (int)Statuses.Open).Any())
            {
                issue.StatusId = (int)Statuses.Resolved;

                issueReposiltory.Save();
            }
        }

        public void Delete(int id)
        {
            SubTask subTask = FindSingleBy(id);
            if (subTask != null)
            {
                subTaskReposiltory.Delete(subTask);
                subTaskReposiltory.Save();
            }

        }

        public bool IsUsed(int id)
        {
            throw new NotImplementedException();
        }
    }
}
