using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using TaskApplication.Common;
using TaskApplication.DataAccess.Entities;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.Services.Interfaces;

namespace TaskApplication.Services.Concrete
{
    public class SubTaskService : ISubTaskService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(SubTaskService));
        
        //private IssueRepository _issueReposiltory = new IssueRepository();
        //private SubTaskRepository _subTaskReposiltory = new SubTaskRepository();

        private readonly IIssueRepository _issueReposiltory = Ioc.Get<IIssueRepository>();
        private readonly ISubTaskRepository _subTaskReposiltory = Ioc.Get<ISubTaskRepository>();

        public IEnumerable<SubTask> GetAll()
        {
            try
            {
                return _subTaskReposiltory.GetAll();
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
                return _subTaskReposiltory.FindBy(s => s.IssueId == id).ToList();
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
                return _subTaskReposiltory.FindSingleBy(s => s.SubTaskId == id, isDetached);
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

                _subTaskReposiltory.Save();
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
                _subTaskReposiltory.Add(subTask);
                _subTaskReposiltory.Save();
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
                _subTaskReposiltory.Edit(subTask);
                _subTaskReposiltory.Save();

                var issue = _issueReposiltory.FindSingleBy(i => i.IssueId == subTask.IssueId);

                if (!issue.SubTasks.Where(s => s.StatusId == (int)Statuses.Open).Any())
                {
                    issue.StatusId = (int)Statuses.Resolved;

                    _issueReposiltory.Save();
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
                    _subTaskReposiltory.Delete(subTask);
                    _subTaskReposiltory.Save();
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
