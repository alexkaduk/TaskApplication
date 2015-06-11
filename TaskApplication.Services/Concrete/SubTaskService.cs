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
            try
            {
                return subTaskReposiltory.GetAll();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new SubTask[0];
            }
        }

        public IEnumerable<SubTask> GetAllByIssueId(int id)
        {
            try
            {
                return subTaskReposiltory.FindBy(s => s.IssueId == id).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new SubTask[0];
            }
        }

        public SubTask FindSingleBy(int id, bool isDetached = false)
        {
            try
            {
                return subTaskReposiltory.FindSingleBy(s => s.SubTaskId == id, isDetached);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new SubTask();
            }
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
            try
            {
                subTaskReposiltory.Add(subTask);
                subTaskReposiltory.Save();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public void Edit(SubTask subTask)
        {
            try
            {
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
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                SubTask subTask = FindSingleBy(id);
                if (subTask != null)
                {
                    subTaskReposiltory.Delete(subTask);
                    subTaskReposiltory.Save();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }
        //public bool IsUsed(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
