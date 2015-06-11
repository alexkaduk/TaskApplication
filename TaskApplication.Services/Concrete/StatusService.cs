using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.Services.Interfaces;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.DataAccess.Entities;
using log4net;

namespace TaskApplication.Services.Concrete
{
    public class StatusService : IStatusService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(StatusService));
        private StatusReposiltory statusReposiltory = new StatusReposiltory();

        public IEnumerable<Status> GetAll()
        {
            try
            {
                return statusReposiltory.GetAll();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new Status[0];
            }
        }

        public Status FindSingleBy(int id)
        {
            try
            {
                return statusReposiltory.FindSingleBy(s => s.StatusId == id);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new Status();
            }
        }
    }
}
