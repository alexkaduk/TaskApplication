using System;
using System.Collections.Generic;
using log4net;
using TaskApplication.Common;
using TaskApplication.DataAccess.Entities;
using TaskApplication.DataAccess.Repositories;
using TaskApplication.Services.Interfaces;

namespace TaskApplication.Services.Concrete
{
    public class StatusService : IStatusService
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(StatusService));
        
        //private StatusRepository _statusReposiltory = new StatusRepository();

        private readonly IStatusRepository _statusReposiltory = Ioc.Get<IStatusRepository>();
        
        public IEnumerable<Status> GetAll()
        {
            try
            {
                return _statusReposiltory.GetAll();
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
                return _statusReposiltory.FindSingleBy(s => s.StatusId == id);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return new Status();
            }
        }
    }
}
