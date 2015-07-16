using System.Collections.Generic;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Interfaces
{
    public interface IStatusService
    {
        IEnumerable<Status> GetAll();
        Status FindSingleBy(int id);
    }
}
