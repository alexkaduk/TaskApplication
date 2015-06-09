using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.Services.Interfaces;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Concrete
{
    public class SubTaskService : ISubTaskService
    {
        // private CategoryReposiltory categoryReposiltory = new CategoryReposiltory();
        // private IssueReposiltory issueReposiltory = new IssueReposiltory();
        private SubTaskReposiltory subTaskReposiltory = new SubTaskReposiltory();

        public IEnumerable<SubTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubTask> GetAllByIssueId(int id)
        {
            return subTaskReposiltory.FindBy(s => s.IssueId == id).ToList();
            //throw new NotImplementedException();
        }

        public SubTask FindSingleBy(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(SubTask subTask)
        {
            throw new NotImplementedException();
        }

        public void Edit(SubTask subTask)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsUsed(int id)
        {
            throw new NotImplementedException();
        }
    }
}
