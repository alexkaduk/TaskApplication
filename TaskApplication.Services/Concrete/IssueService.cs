﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.Services.Interfaces;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Concrete
{
    public class IssueService : IIssueService
    {
        private IssueReposiltory issueReposiltory = new IssueReposiltory();
        private StatusReposiltory statusReposiltory = new StatusReposiltory();
        private SubTaskReposiltory subTaskReposiltory = new SubTaskReposiltory();

        public IEnumerable<Issue> GetAll()
        {
            return issueReposiltory.GetAll();
        }

        public Issue FindSingleBy(int id)
        {
            return issueReposiltory.FindSingleBy(i => i.IssueId == id);
        }

        public void Add(Issue issue)
        {
            issue.IssueCreateDate = DateTime.Now;
            issue.IssueUpdateDate = DateTime.Now;
            issue.StatusId = statusReposiltory.FindSingleBy(s => s.StatusName == "Open").StatusId;

            issueReposiltory.Add(issue);
            issueReposiltory.Save();
        }

        public void Edit(Issue issue)
        {
            //categoryReposiltory.Edit(category);
            //categoryReposiltory.Save();
           
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
            Issue oldIssue = issueReposiltory.FindSingleBy(i => i.IssueId == issue.IssueId);
            // db.Entry(oldIssue).State = EntityState.Detached;

            issue.IssueCreateDate = oldIssue.IssueCreateDate;
            issue.IssueUpdateDate = DateTime.Now;

            issueReposiltory.Edit(issue);
            issueReposiltory.Save();
        }

        public void Delete(int id)
        {
            //Category category = FindSingleBy(id);
            //if (category != null)
            //{
            //    categoryReposiltory.Delete(category);
            //    categoryReposiltory.Save();
            //}
            Issue issue = FindSingleBy(id);
            if (issue != null)
            {
                issueReposiltory.Delete(issue);
                issueReposiltory.Save();
            }
            
        }

        public bool IsUsed(int id)
        {
            return subTaskReposiltory.FindBy(s => s.IssueId == id).Any();
        }

        public bool IsAnyResolved()
        {
            //!db.Issues.Any(i => i.StatusId == (int)Statuses.Resolved)
            return issueReposiltory.FindBy(i => i.StatusId == (int)Statuses.Resolved).Any();
        }
    }
}