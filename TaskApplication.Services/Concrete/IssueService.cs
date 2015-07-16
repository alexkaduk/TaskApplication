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
    public class IssueService : IIssueService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(IssueService));
        
        //private IssueRepository _issueReposiltory = new IssueRepository();
        //private StatusRepository _statusReposiltory = new StatusRepository();
        //private SubTaskRepository _subTaskReposiltory = new SubTaskRepository();

        private readonly IIssueRepository _issueReposiltory = Ioc.Get<IIssueRepository>();
        private readonly IStatusRepository _statusReposiltory = Ioc.Get<IStatusRepository>();
        private readonly ISubTaskRepository _subTaskReposiltory = Ioc.Get<ISubTaskRepository>();

        public IEnumerable<Issue> GetAll()
        {
            try
            {
                return _issueReposiltory.GetAll().OrderBy(i => i.CategoryId);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new Issue[0];
            }

        }

        public Issue FindSingleBy(int id)
        {
            try
            {
                return _issueReposiltory.FindSingleBy(i => i.IssueId == id);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new Issue();
            }

        }

        public void Add(Issue issue)
        {
            try
            {
                issue.IssueCreateDate = issue.IssueUpdateDate = DateTime.Now;
                issue.StatusId = _statusReposiltory.FindSingleBy(s => s.StatusName == "Open").StatusId;

                _issueReposiltory.Add(issue);
                _issueReposiltory.Save();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public void Edit(Issue issue)
        {
            try
            {
                Issue oldIssue = _issueReposiltory.FindSingleBy(i => i.IssueId == issue.IssueId, true);

                issue.IssueCreateDate = oldIssue.IssueCreateDate;
                issue.IssueUpdateDate = DateTime.Now;

                _issueReposiltory.Edit(issue);
                _issueReposiltory.Save();
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
                Issue issue = FindSingleBy(id);
                if (issue != null)
                {
                    _issueReposiltory.Delete(issue);
                    _issueReposiltory.Save();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public bool IsUsed(int id)
        {
            try
            {
                return _subTaskReposiltory.FindBy(s => s.IssueId == id).Any();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }

        public bool IsAnyResolved()
        {
            try
            {
                return _issueReposiltory.FindBy(i => i.StatusId == (int)Statuses.Resolved).Any();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }

        public IEnumerable<Issue> GetAllResolved()
        {
            try
            {
                return _issueReposiltory.FindBy(i => i.StatusId == (int)Statuses.Resolved).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new Issue[0];
            }
        }

        public void DeleteAllResolved()
        {
            try
            {
                var resovledIssues = GetAllResolved();
                foreach (Issue issue in resovledIssues)
                {
                    foreach (SubTask subtask in issue.SubTasks.ToArray())
                    {
                        _subTaskReposiltory.Delete(subtask);
                    }
                    _issueReposiltory.Delete(issue);
                }
                _subTaskReposiltory.Save();
                _issueReposiltory.Save();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public bool ChangeStatusIfAllSubTasksResolved(Issue issue)
        {
            try
            {
                if (issue.SubTasks.All(s => s.StatusId == (int)Statuses.Resolved))
                {
                    issue.StatusId = (int)Statuses.Resolved;
                    _issueReposiltory.Save();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
        }
    }
}
