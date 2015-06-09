using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Interfaces
{
    interface IStatusService
    {
        IEnumerable<Status> GetAll();
        Status FindSingleBy(int id);
    }
}
