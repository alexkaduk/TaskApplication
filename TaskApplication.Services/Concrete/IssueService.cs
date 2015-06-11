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
    public class IssueService : IIssueService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(IssueService));
        private IssueReposiltory issueReposiltory = new IssueReposiltory();
        private StatusReposiltory statusReposiltory = new StatusReposiltory();
        private SubTaskReposiltory subTaskReposiltory = new SubTaskReposiltory();

        public IEnumerable<Issue> GetAll()
        {
            try
            {
                return issueReposiltory.GetAll().OrderBy(i => i.CategoryId);
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
                return issueReposiltory.FindSingleBy(i => i.IssueId == id);
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
                issue.StatusId = statusReposiltory.FindSingleBy(s => s.StatusName == "Open").StatusId;

                issueReposiltory.Add(issue);
                issueReposiltory.Save();
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
                Issue oldIssue = issueReposiltory.FindSingleBy(i => i.IssueId == issue.IssueId, true);

                issue.IssueCreateDate = oldIssue.IssueCreateDate;
                issue.IssueUpdateDate = DateTime.Now;

                issueReposiltory.Edit(issue);
                issueReposiltory.Save();
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
                    issueReposiltory.Delete(issue);
                    issueReposiltory.Save();
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
                return subTaskReposiltory.FindBy(s => s.IssueId == id).Any();
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
                return issueReposiltory.FindBy(i => i.StatusId == (int)Statuses.Resolved).Any();
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
                return issueReposiltory.FindBy(i => i.StatusId == (int)Statuses.Resolved).ToList();
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
                    foreach (var subtask in issue.SubTasks.ToArray())
                    {
                        subTaskReposiltory.Delete(subtask);
                    }
                    issueReposiltory.Delete(issue);
                }
                subTaskReposiltory.Save();
                issueReposiltory.Save();
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
                    issueReposiltory.Save();
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
