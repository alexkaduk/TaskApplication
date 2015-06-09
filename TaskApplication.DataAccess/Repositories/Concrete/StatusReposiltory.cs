using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.DataAccess.Entities;
using TaskApplication.Models;

namespace TaskApplication.DataAccess.Repositories
{
    public class StatusReposiltory :
    GenericRepository<TaskContex, Status>, IStatusRepository
    {
    }
}
