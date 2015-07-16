using System.Collections.Generic;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Interfaces
{
    public interface ISubTaskService
    {
        IEnumerable<SubTask> GetAll();
        IEnumerable<SubTask> GetAllByIssueId(int id);
        SubTask FindSingleBy(int id, bool isDetached);
        void ChangeStatusOpenResolve(SubTask subTask);
        void Add(SubTask subTask);
        void Edit(SubTask subTask);
        void Delete(int id);

        //public SubTask FindSingleBy(int id, bool isDetached = false)
        //bool IsUsed(int id);
    }
}
