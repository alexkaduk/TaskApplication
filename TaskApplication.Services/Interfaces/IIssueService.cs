using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Interfaces
{
    interface IIssueService
    {
        IEnumerable<Issue> GetAll();
        Issue FindSingleBy(int id);
        void Add(Issue issue);
        void Edit(Issue issue);
        void Delete(int id);
        bool IsUsed(int id);
        bool IsAnyResolved();
    }
}
