using System.Collections.Generic;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category FindSingleBy(int id);
        void Add(Category category);
        void Edit(Category category);
        void Delete(int id);
        bool IsUsed(int id);
    }
}
