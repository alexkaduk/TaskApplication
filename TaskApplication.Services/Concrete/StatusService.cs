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
    public class StatusService : IStatusService
    {
        private StatusReposiltory statusReposiltory = new StatusReposiltory();
        
        public IEnumerable<Status> GetAll()
        {
            return statusReposiltory.GetAll();
        }

        public Status FindSingleBy(int id)
        {
            return statusReposiltory.FindSingleBy(s => s.StatusId == id);
        }
    }
}
