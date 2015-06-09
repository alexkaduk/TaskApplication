using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Interfaces
{
    interface ISubTaskService
    {
        IEnumerable<SubTask> GetAll();
        IEnumerable<SubTask> GetAllByIssueId(int id);
        SubTask FindSingleBy(int id);
        void Add(SubTask subTask);
        void Edit(SubTask subTask);
        void Delete(int id);
        bool IsUsed(int id);
    }
}
