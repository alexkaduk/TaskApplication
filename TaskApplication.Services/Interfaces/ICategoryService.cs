using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApplication.DataAccess.Entities;

namespace TaskApplication.Services.Interfaces
{
    interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category FindSingleBy(int id);
        void Add(Category category);
        void Edit(Category category);
        void Delete(int id);
        bool IsUsed(int id);
    }
}
