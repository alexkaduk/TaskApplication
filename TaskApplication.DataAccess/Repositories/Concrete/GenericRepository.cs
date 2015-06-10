using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.DataAccess.Repositories
{
    public abstract class GenericRepository<C, T> :
    IGenericRepository<T>
        where T : class
        where C : DbContext, new()
    {

        private C _entities = new C();
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public virtual IQueryable<Status> GetAllStatuses()
        {
            IQueryable<Status> query = _entities.Set<Status>();
            return query;
        }

        public T FindSingleBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool isDetached = false)
        {
            T query = _entities.Set<T>().Where(predicate).FirstOrDefault();
            if (isDetached)
            {
                _entities.Entry(query).State = EntityState.Detached;
            }
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}
