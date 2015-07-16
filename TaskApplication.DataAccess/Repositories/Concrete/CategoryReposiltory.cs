using TaskApplication.DataAccess.Entities;
using TaskApplication.Models;

namespace TaskApplication.DataAccess.Repositories
{
    public class CategoryRepository :
    GenericRepository<TaskContex, Category>, ICategoryRepository
    {
    }
}
