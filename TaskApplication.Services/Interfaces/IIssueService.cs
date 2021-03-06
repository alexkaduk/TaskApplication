﻿using System.Collections.Generic;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Interfaces
{
    public interface IIssueService
    {
        IEnumerable<Issue> GetAll();
        Issue FindSingleBy(int id);
        void Add(Issue issue);
        void Edit(Issue issue);
        void Delete(int id);
        bool IsUsed(int id);
        bool IsAnyResolved();
        IEnumerable<Issue> GetAllResolved();
        void DeleteAllResolved();
        bool ChangeStatusIfAllSubTasksResolved(Issue issue);
    }
}
